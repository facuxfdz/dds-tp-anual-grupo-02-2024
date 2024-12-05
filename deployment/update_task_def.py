import os
import json
import boto3


def get_secrets(secret_name, region_name):
    """Retrieve secret values from AWS Secrets Manager."""
    session = boto3.session.Session()
    client = session.client(service_name='secretsmanager', region_name=region_name)
    response = client.get_secret_value(SecretId=secret_name)
    return json.loads(response['SecretString'])


def load_task_definition(task_definition_file):
    """Load the ECS task definition from a JSON file."""
    with open(task_definition_file, 'r') as file:
        return json.load(file)


def override_task_definition(task_def, overrides):
    """Apply overrides to the ECS task definition."""
    for key, value_fn in overrides.items():
        path, attribute = key.rsplit('.', 1)
        current = task_def
        for part in path.split('.'):
            if part.isdigit():
                part = int(part)
            current = current[part]
        current[attribute] = value_fn()
    return task_def


def register_task_definition(task_definition):
    """Register an ECS task definition."""
    ecs_client = boto3.client('ecs', region_name=os.getenv('AWS_REGION', 'us-east-1'))
    print(f"Registering task definition {task_definition['family']}...")
    print(f"Image: {task_definition['containerDefinitions'][0]['image']}")
    response = ecs_client.register_task_definition(**task_definition)
    print(f"Task Definition {response['taskDefinition']['taskDefinitionArn']} registered.")
    return response['taskDefinition']['taskDefinitionArn']


def create_alb_target_group(elbv2_client, service_name, vpc_id, target_port):
    """Create an ALB Target Group."""
    print(f"Creating ALB Target Group for service {service_name}...")
    # Replace _ with - because the name must match the regex: [a-zA-Z0-9-]+
    service_valid_name = service_name.replace('_', '-')
    response = elbv2_client.create_target_group(
        Name=f"{service_valid_name}-tg",
        Protocol='HTTP',
        Port=target_port,
        VpcId=vpc_id,
        TargetType='ip'
    )
    tg_arn = response['TargetGroups'][0]['TargetGroupArn']
    print(f"Target Group created: {tg_arn}")
    return tg_arn


def attach_tg_to_service(elbv2_client, alb_arn, tg_arn, host, priority):
    """Attach TG to the ALB with host-based routing."""
    print(f"Adding routing rule for host {host} to ALB {alb_arn}...")
    elbv2_client.create_rule(
        ListenerArn=alb_arn,
        Conditions=[
            {'Field': 'host-header', 'HostHeaderConfig': {'Values': [host]}}
        ],
        Actions=[
            {'Type': 'forward', 'ForwardConfig': {'TargetGroups': [{'TargetGroupArn': tg_arn, 'Weight': 1}]}}
        ],
        Priority=priority
    )
    print(f"Routing rule for host {host} added to ALB.")


def service_exists(ecs_client, cluster_name, service_name):
    """Check if the ECS service already exists."""
    try:
        res = ecs_client.describe_services(cluster=cluster_name, services=[service_name])
        return res['services'][0]['status'] == 'ACTIVE'
    except Exception as e:
        print(f"Service {service_name} does not exist: {e}")
        return False


def create_service(
        cluster_name,
        service_name,
        task_definition_arn,
        tg_arn,
        subnet_ids,
        security_groups,
        port,
        exposed=True
        ):
    """Create or update an ECS service."""
    ecs_client = boto3.client('ecs', region_name=os.getenv('AWS_REGION', 'us-east-1'))
    print(f"Deploying service {service_name} in cluster {cluster_name}")

    if service_exists(ecs_client, cluster_name, service_name):
        print(f"Service {service_name} already exists. Updating...")
        response = ecs_client.update_service(
            cluster=cluster_name,
            service=service_name,
            taskDefinition=task_definition_arn,
            desiredCount=1,
            loadBalancers=[
                {
                    'targetGroupArn': tg_arn,
                    'containerName': service_name,
                    'containerPort': port
                }
            ]
        )
        print(f"Service {service_name} updated: {response['service']['serviceArn']}")
    else:
        print(f"Creating service {service_name}...")
        response = ecs_client.create_service(
            cluster=cluster_name,
            serviceName=service_name,
            taskDefinition=task_definition_arn,
            desiredCount=1,
            launchType='FARGATE',
            networkConfiguration={
                'awsvpcConfiguration': {
                    'subnets': subnet_ids,
                    'securityGroups': security_groups,
                }
            },
            serviceConnectConfiguration={
                'enabled': True,
                'discoveryName': service_name,
                'clientAliases': [{'dnsName': f'{service_name}.svc.local', 'port': port}]
            } if exposed else None,
            loadBalancers=[
                {
                    'targetGroupArn': tg_arn,
                    'containerName': service_name,
                    'containerPort': port
                }
            ]
        )
        print(f"Service {service_name} created: {response['service']['serviceArn']}")


if __name__ == '__main__':
    REGION = os.getenv('AWS_REGION', 'us-east-1')
    VPC_ID = os.getenv('VPC_ID', "vpc-07ce9852c2808c23a")
    ALB_ARN = os.getenv('ALB_ARN',"arn:aws:elasticloadbalancing:us-east-1:034781041905:listener/app/acceso-alimentario/8f9aac2357213e8a/5ed28c68b2124861")
    CLUSTER_NAME = 'accesoalimentario'
    TASK_DEFINITION_DIR = 'task_defs'
    subnet_ids = ['subnet-0a094580305b044f2', 'subnet-0e147b4b3ad9a5a02', 'subnet-07b5be1eae03a7cb3']
    security_groups = ['sg-047447291c2271910']

    os.environ["FRONT_IMAGE"] = "034781041905.dkr.ecr.us-east-1.amazonaws.com/acceso-alimentario_releases/front:ea80f21c0d6e0640198a4d362d530a4cea0089b7"
    os.environ["BACK_IMAGE"] = "034781041905.dkr.ecr.us-east-1.amazonaws.com/acceso-alimentario_releases:ea80f21c0d6e0640198a4d362d530a4cea0089b7"
    os.environ["RECOMENDACIONES_API_IMAGE"] = "034781041905.dkr.ecr.us-east-1.amazonaws.com/acceso-alimentario_releases/recomendaciones-api:ea80f21c0d6e0640198a4d362d530a4cea0089b7"

    NOT_EXPOSED_SERVICES = ["rabbitmq"]
    HOST_MAPPING = {
        "frontend": "acceso-alimentario.opsconsultingservices.com",
        "backend": "back.acceso-alimentario.opsconsultingservices.com",
        "recomendaciones_api": "recomendaciones.acceso-alimentario.opsconsultingservices.com/"
    }
    SERVICE_MAPPING = {
        "frontend": {
            "containerDefinitions.0.image": lambda: os.getenv("FRONT_IMAGE")
        },
        "backend": {
            "containerDefinitions.0.image": lambda: os.getenv("BACK_IMAGE")
        },
        "recomendaciones_api": {
            "containerDefinitions.0.image": lambda: os.getenv("RECOMENDACIONES_API_IMAGE")
        },
        "rabbitmq": {
            "containerDefinitions.0.image": lambda: "rabbitmq:3.8-management-alpine"
        }
    }

    elbv2_client = boto3.client('elbv2', region_name=REGION)
    priority = 1

    for service_name, overrides in SERVICE_MAPPING.items():
        task_def_file = os.path.join(TASK_DEFINITION_DIR, f"{service_name}.json")
        print(f"Processing service: {service_name}")
        task_def = load_task_definition(task_def_file)
        updated_task_def = override_task_definition(task_def, overrides)
        task_def_arn = register_task_definition(updated_task_def)
        exposed = service_name not in NOT_EXPOSED_SERVICES
        
        target_port = updated_task_def['containerDefinitions'][0]['portMappings'][0]['containerPort']
        tg_arn = create_alb_target_group(elbv2_client, service_name, VPC_ID, target_port)
        host = HOST_MAPPING[service_name]
        attach_tg_to_service(elbv2_client, ALB_ARN, tg_arn, host, priority)
        priority += 1
        create_service(CLUSTER_NAME, service_name, task_def_arn, tg_arn, subnet_ids, security_groups,port=target_port,exposed=exposed)