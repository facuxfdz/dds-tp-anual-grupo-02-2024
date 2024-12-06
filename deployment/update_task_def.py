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
            ] if exposed else []
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
                'namespace': 'accesoalimentario_namespace',
                'services': [
                    {
                        'portName': 'http',
                        'discoveryName': service_name,
                        'clientAliases': [{
                            'port': port,
                            'dnsName': f"{service_name}.accesoalimentario_namespace"
                        }]
                    }
                ]
            } if exposed else {
                'enabled': False
            },
            loadBalancers=[
                {
                    'targetGroupArn': tg_arn,
                    'containerName': service_name,
                    'containerPort': port
                }
            ] if exposed else []
        )
        print(f"Service {service_name} created: {response['service']['serviceArn']}")

def get_existing_target_group(elbv2_client, service_name):
    """Check if a Target Group already exists."""
    service_valid_name = service_name.replace('_', '-')
    response = elbv2_client.describe_target_groups(Names=[f"{service_valid_name}-tg"])
    if response['TargetGroups']:
        return response['TargetGroups'][0]['TargetGroupArn']
    return None


def create_or_get_alb_target_group(elbv2_client, service_name, vpc_id, target_port):
    """Create or get an ALB Target Group."""
    print(f"Checking if Target Group for service {service_name} exists...")
    try:
        tg_arn = get_existing_target_group(elbv2_client, service_name)
    except Exception as e:
        print(f"Error getting Target Group: {e}")
        tg_arn = None
    if tg_arn:
        print(f"Target Group already exists: {tg_arn}")
        return tg_arn

    print(f"Creating ALB Target Group for service {service_name}...")
    service_valid_name = service_name.replace('_', '-')
    response = elbv2_client.create_target_group(
        Name=f"{service_valid_name}-tg",
        Protocol='HTTP',
        Port=target_port,
        VpcId=vpc_id,
        TargetType='ip',
        Matcher={'HttpCode': '200-499'}
    )
    tg_arn = response['TargetGroups'][0]['TargetGroupArn']
    print(f"Target Group created: {tg_arn}")
    return tg_arn


def rule_exists(elbv2_client, alb_arn, host):
    """Check if a Rule for the given host-header already exists."""
    response = elbv2_client.describe_rules(ListenerArn=alb_arn)
    for rule in response['Rules']:
        for condition in rule.get('Conditions', []):
            if condition['Field'] == 'host-header' and host in condition['HostHeaderConfig']['Values']:
                return rule['RuleArn']
    return None


def attach_or_get_tg_to_service(elbv2_client, alb_arn, tg_arn, host, priority):
    """Attach TG to the ALB with host-based routing if not already attached."""
    print(f"Checking if routing rule for host {host} exists...")
    rule_arn = rule_exists(elbv2_client, alb_arn, host)
    if rule_arn:
        print(f"Routing rule for host {host} already exists: {rule_arn}")
        return rule_arn

    print(f"Adding routing rule for host {host} to ALB {alb_arn}...")
    response = elbv2_client.create_rule(
        ListenerArn=alb_arn,
        Conditions=[
            {'Field': 'host-header', 'HostHeaderConfig': {'Values': [host]}}
        ],
        Actions=[
            {'Type': 'forward', 'ForwardConfig': {'TargetGroups': [{'TargetGroupArn': tg_arn, 'Weight': 1}]}}
        ],
        Priority=priority
    )
    rule_arn = response['Rules'][0]['RuleArn']
    print(f"Routing rule for host {host} added: {rule_arn}")
    return rule_arn

def get_vpc_id():
    """Retrieve VPC ID from AWS By using the name."""
    vpc_name = "acceso-alimentario"
    ec2_client = boto3.client('ec2', region_name=os.getenv('AWS_REGION', 'us-east-1'))
    response = ec2_client.describe_vpcs(Filters=[{'Name': 'tag:Name', 'Values': [vpc_name]}])
    if not response['Vpcs']:
        raise ValueError(f"No VPC found with name {vpc_name}.")
    return response['Vpcs'][0]['VpcId']


def get_alb_arn(elbv2_client, alb_name):
    """Retrieve ALB 443 listener ARN by name."""
    response = elbv2_client.describe_load_balancers(Names=[alb_name])
    if not response['LoadBalancers']:
        raise ValueError(f"No ALB found with name {alb_name}.")
    alb_arn = response['LoadBalancers'][0]['LoadBalancerArn']
    # retrieve 443 listener ARN
    response = elbv2_client.describe_listeners(LoadBalancerArn=alb_arn)
    for listener in response['Listeners']:
        if listener['Port'] == 443:
            return listener['ListenerArn']
    raise ValueError(f"No 443 listener found in ALB {alb_name}.")


def get_private_subnets(ec2_client, vpc_id):
    """Retrieve private subnet IDs by VPC ID."""
    response = ec2_client.describe_subnets(
        Filters=[
            {'Name': 'vpc-id', 'Values': [vpc_id]},
            {'Name': 'tag:Name', 'Values': ['*-private']}
        ]
    )
    subnet_ids = [subnet['SubnetId'] for subnet in response['Subnets']]
    if not subnet_ids:
        raise ValueError(f"No private subnets found in VPC {vpc_id}.")
    return subnet_ids


def get_image_env_var(env_var_name):
    """Retrieve image value from environment variables."""
    image = os.getenv(env_var_name)
    if not image:
        raise ValueError(f"{env_var_name} environment variable is not set.")
    return image


def main():
    REGION = os.getenv('AWS_REGION', 'us-east-1')
    CLUSTER_NAME = 'accesoalimentario'
    TASK_DEFINITION_DIR = 'task_defs'
    alb_name = "acceso-alimentario"

    # AWS clients
    ec2_client = boto3.client('ec2', region_name=REGION)
    elbv2_client = boto3.client('elbv2', region_name=REGION)

    # Dynamic values
    VPC_ID = get_vpc_id()
    ALB_ARN = get_alb_arn(elbv2_client, alb_name)
    subnet_ids = get_private_subnets(ec2_client, VPC_ID)

    # Set images dynamically
    os.environ["BACK_IMAGE"] = get_image_env_var("BACK_IMAGE")
    os.environ["FRONT_IMAGE"] = get_image_env_var("FRONT_IMAGE")
    os.environ["RECOMENDACIONES_API_IMAGE"] = get_image_env_var("RECOMENDACIONES_API_IMAGE")

    security_groups = ['sg-047447291c2271910']
    NOT_EXPOSED_SERVICES = ["rabbitmq"]
    HOST_MAPPING = {
        "frontend": "acceso-alimentario.opsconsultingservices.com",
        "backend": "back.acceso-alimentario.opsconsultingservices.com",
        "recomendaciones_api": "recomendaciones.acceso-alimentario.opsconsultingservices.com"
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

    priority = 1

    for service_name, overrides in SERVICE_MAPPING.items():
        task_def_file = os.path.join(TASK_DEFINITION_DIR, f"{service_name}.json")
        print(f"Processing service: {service_name}")
        task_def = load_task_definition(task_def_file)
        updated_task_def = override_task_definition(task_def, overrides)
        task_def_arn = register_task_definition(updated_task_def)
        exposed = service_name not in NOT_EXPOSED_SERVICES

        target_port = updated_task_def['containerDefinitions'][0]['portMappings'][0]['containerPort']
        host = HOST_MAPPING.get(service_name, None)
        tg_arn = None
        if exposed:
            tg_arn = create_or_get_alb_target_group(elbv2_client, service_name, VPC_ID, target_port)
            attach_or_get_tg_to_service(elbv2_client, ALB_ARN, tg_arn, host, priority)
        priority += 1
        create_service(CLUSTER_NAME, service_name, task_def_arn, tg_arn, subnet_ids, security_groups, port=target_port, exposed=exposed)


if __name__ == '__main__':
    main()