import os
import json
import boto3

def get_secrets(secret_name, region_name):
    """Retrieve secret values from AWS Secrets Manager."""
    client = boto3.client('secretsmanager', region_name=region_name)
    response = client.get_secret_value(SecretId=secret_name)
    return json.loads(response['SecretString'])

def update_task_definition(task_definition_file, output_file, region, rabbit_secret_name):
    """Update the ECS task definition with container images and environment variables."""
    # Load the current task definition
    print(f"Loading task definition from {task_definition_file}")
    with open(task_definition_file, 'r') as file:
        task_def = json.load(file)

    # Get RabbitMQ secrets
    rabbit_secrets = get_secrets(rabbit_secret_name, region)
    rabbit_user = rabbit_secrets['username']
    rabbit_pass = rabbit_secrets['password']

    # Replace container image placeholders and add environment variables
    for container in task_def['containerDefinitions']:
        container_name = container['name']
        if container_name == 'rabbitmq':
            container['environment'] = container.get('environment', [])
            container['environment'].append({'name': 'RABBITMQ_DEFAULT_USER', 'value': rabbit_user})
            container['environment'].append({'name': 'RABBITMQ_DEFAULT_PASS', 'value': rabbit_pass})
        elif container_name == 'frontend':
            container['image'] = os.environ.get('full_image_front')
        elif container_name == 'recomendaciones-api':
            container['image'] = os.environ.get('full_image_recomendaciones-api')
        elif container_name == 'backend':
            container['image'] = os.environ.get('full_image')

    # Write updated task definition to output file
    with open(output_file, 'w') as file:
        json.dump(task_def, file, indent=4)

    print(f"Updated task definition saved to {output_file}")

if __name__ == '__main__':
    # Configuration
    REGION = os.environ.get('AWS_REGION', 'us-east-1')
    current_dir = os.path.dirname(os.path.realpath(__file__))
    TASK_DEFINITION_FILE = os.environ.get('ECS_TASK_DEFINITION', os.path.join(current_dir, 'task_def.json'))
    OUTPUT_FILE = 'task_def_updated.json'
    RABBIT_SECRET_NAME = 'rabbitmq'

    # Update the ECS task definition
    update_task_definition(TASK_DEFINITION_FILE, OUTPUT_FILE, REGION, RABBIT_SECRET_NAME)
