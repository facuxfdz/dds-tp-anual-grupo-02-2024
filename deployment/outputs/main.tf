// Create secret for rabbitmq connection data
resource "aws_secretsmanager_secret" "rabbitmq" {
  name = "rabbitmq"
}

// generate random password for rabbitmq
resource "random_password" "rabbitmq" {
  length           = 16
  special          = true
  override_special = "_%@"
}

resource "aws_secretsmanager_secret_version" "rabbitmq" {
  secret_id     = aws_secretsmanager_secret.rabbitmq.id
  secret_string = jsonencode({
    host     = var.rabbitmq_host
    username = var.rabbitmq_username
    password = random_password.rabbitmq.result
  })
}