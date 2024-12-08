data "aws_subnets" "private" {
  filter {
    name   = "tag:Name"
    values = ["${var.vpc_name}-private"]
  }
}

resource "aws_iam_policy" "secrets_manager" {
  name        = "secrets_manager_policy"
  description = "Policy to allow ECS task to access secrets manager"
  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Effect = "Allow",
        Action = [
          "secretsmanager:*",
          "ssm:*",
        ],
        Resource = "*",
      },
    ],
  })
}

data "aws_vpc" "vpc" {
  filter {
    name   = "tag:Name"
    values = [var.vpc_name]
  }
}

# Create namespace
resource "aws_service_discovery_private_dns_namespace" "namespace" {
  name = "accesoalimentario_namespace"
  vpc  = data.aws_vpc.vpc.id
}

# IAM Role for task role
resource "aws_iam_role" "task_role" {
  name = "accesoalimentario_task_role"
  assume_role_policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Effect = "Allow",
        Principal = {
          Service = "ecs-tasks.amazonaws.com",
        },
        Action = "sts:AssumeRole",
      },
    ],
  })
}

# Attach policy to task role
resource "aws_iam_role_policy_attachment" "secrets_manager" {
  role       = aws_iam_role.task_role.name
  policy_arn = aws_iam_policy.secrets_manager.arn
}

# Security group for tasks
resource "aws_security_group" "task_sg" {
  name        = "accesoalimentario_task_sg"
  description = "Security group for ECS tasks"
  vpc_id      = data.aws_vpc.vpc.id
  tags = {
    Name = "accesoalimentario_task_sg"
  }
}

resource "aws_vpc_security_group_ingress_rule" "task_sg_ingress" {
  security_group_id = aws_security_group.task_sg.id
  ip_protocol       = "-1"
  cidr_ipv4         = "0.0.0.0/0"
}


module "ecs" {
  source  = "terraform-aws-modules/ecs/aws"
  version = "5.11.4"

  cluster_name = var.cluster_name

  create_task_exec_iam_role = true
  create_task_exec_policy   = true
  task_exec_iam_role_policies = {
    logs = "arn:aws:iam::aws:policy/CloudWatchLogsFullAccess"
  }
  task_exec_iam_role_name = "${var.service_name}-task-exec-role"
  cluster_service_connect_defaults = {
    namespace = aws_service_discovery_private_dns_namespace.namespace.arn
  }
  fargate_capacity_providers = {
    FARGATE = {
      default_capacity_provider_strategy = {
        weight = 10
      }
    }
    FARGATE_SPOT = {
      default_capacity_provider_strategy = {
        weight = 90
      }
    }
  }


  tags = {
    Terraform = "true"
  }
}
