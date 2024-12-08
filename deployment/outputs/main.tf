// Create secret for rabbitmq connection data
resource "aws_secretsmanager_secret" "rabbitmq" {
  name_prefix = "rabbitmq"
}

// generate random password for rabbitmq
resource "random_password" "rabbitmq" {
  length           = 16
  special          = true
  override_special = "_%@"
}

resource "aws_secretsmanager_secret_version" "rabbitmq" {
  secret_id = aws_secretsmanager_secret.rabbitmq.id
  secret_string = jsonencode({
    host     = var.rabbitmq_host
    username = var.rabbitmq_username
    password = random_password.rabbitmq.result
  })
}

# SSM Parameter for secret name
resource "aws_ssm_parameter" "rabbitmq_secret_name" {
  name  = "/accesoalimentario/rabbitmq/secret_name"
  type  = "String"
  value = aws_secretsmanager_secret.rabbitmq.name
}