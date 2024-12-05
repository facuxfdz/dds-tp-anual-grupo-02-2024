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
        # Traverse the task_def dictionary to find the correct location
        current = task_def
        for part in path.split('.'):
            # If part is a number, we need to convert it to an integer
            if part.isdigit():
                part = int(part)
            current = current[part]
        # Apply the override
        current[attribute] = value_fn()
    return task_def

def register_task_definition(task_definition):
    """Register an ECS task definition."""
    ecs_client = boto3.client('ecs', region_name=os.getenv('AWS_REGION', 'us-east-1'))
    response = ecs_client.register_task_definition(**task_definition)
    print(f"Task Definition {response['taskDefinition']['taskDefinitionArn']} registered.")
    return response['taskDefinition']['taskDefinitionArn']


def create_service(
        cluster_name, 
        service_name, 
        task_definition_arn, 
        namespace, 
        port, 
        subnet_ids,
        security_groups,
        exposed=False
        ):
    """Create an ECS service with Service Connect enabled."""
    ecs_client = boto3.client('ecs', region_name=os.getenv('AWS_REGION', 'us-east-1'))
    print(f"Creating service {service_name} in cluster {cluster_name}")
    # If exposed is False, we don't need to create the service with Service Connect
    if exposed:
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
                'namespace': namespace,
                'services': [
                    {
                        'portName': 'http',  # Define the port name to be used by Service Connect
                        'discoveryName': service_name,
                        'clientAliases': [{'dnsName': f'{service_name}.svc.local', 'port': port}]
                    }
                ]
            },
        )
    else:
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
        )

    print(f"Service {service_name} created: {response['service']['serviceArn']}")
    return response['service']['serviceArn']


if __name__ == '__main__':
    # Configuration
    REGION = os.getenv('AWS_REGION', 'us-east-1')
    CLUSTER_NAME = 'accesoalimentario'
    NAMESPACE = 'accesoalimentario_namespace'
    TASK_DEFINITION_DIR = 'task_defs'
    subnet_ids = ['subnet-0a094580305b044f2','subnet-0e147b4b3ad9a5a02', 'subnet-07b5be1eae03a7cb3']
    security_groups = ['sg-047447291c2271910']
    
    # start TEST
    # For testing we'll set the env vars here
    os.environ["FRONT_IMAGE"] = "frontend:latest"
    os.environ["BACK_IMAGE"] = "backend:latest"
    os.environ["RECOMENDACIONES_API_IMAGE"] = "recomendaciones_api:latest"
    # end TEST

    NOT_EXPOSED_SERVICES = ["rabbitmq"]
    # Service Mapping
    SERVICE_MAPPING = {
        "frontend": {
            "containerDefinitions.0.image": lambda: os.getenv("FRONT_IMAGE"),
            "containerDefinitions.0.environment": lambda: [
                {"name": "FRONT_ENV_VAR", "value": "value_from_env"}
            ],
        },
        "backend": {
            "containerDefinitions.0.image": lambda: os.getenv("BACK_IMAGE"),
        },
        "rabbitmq": {
            "containerDefinitions.0.environment": lambda: [
                {"name": "RABBITMQ_USER", "value": get_secrets("rabbitmq", REGION)["username"]},
                {"name": "RABBITMQ_PASS", "value": get_secrets("rabbitmq", REGION)["password"]},
            ],
        },
        "recomendaciones_api": {
            "containerDefinitions.0.image": lambda: os.getenv("RECOMENDACIONES_API_IMAGE")
        },
    }

    # Iterate over services and deploy
    for service_name, overrides in SERVICE_MAPPING.items():
        # Load and override task definition
        task_def_file = os.path.join(TASK_DEFINITION_DIR, f"{service_name}.json")
        print(f"Processing service: {service_name}")
        task_def = load_task_definition(task_def_file)
        
        updated_task_def = override_task_definition(task_def, overrides)

        port = updated_task_def['containerDefinitions'][0]['portMappings'][0]['containerPort']
        print(f"Service {service_name} will be exposed on port {port}")
        
        # Register task definition
        task_def_arn = register_task_definition(updated_task_def)
        
        # Create service with Service Connect
        exposed = service_name not in NOT_EXPOSED_SERVICES
        create_service(CLUSTER_NAME, service_name, task_def_arn, NAMESPACE, port, subnet_ids, security_groups, exposed)
