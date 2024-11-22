data "aws_lb_target_group" "alb_tg" {
  name = var.alb_name
}
data "aws_subnets" "private" {
  filter {
    name   = "tag:Name"
    values = ["${var.vpc_name}-private"]
  }
}

data "aws_iam_role" "task_role_arn" {
  name       = var.service_name
  depends_on = [module.ecs]
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
        ],
        Resource = "*",
      },
    ],
  })
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

  services = {
    front_svc = {
      name                           = "${var.service_name}-front"
      tasks_iam_role_use_name_prefix = false
      ignore_task_definition_changes = true
      cpu                            = 512
      memory                         = 1024
      subnet_ids                     = data.aws_subnets.private.ids
      tasks_iam_role_policies = {
        secrets_manager = aws_iam_policy.secrets_manager.arn
      }
      load_balancer = {
        service = {
          target_group_arn = data.aws_lb_target_group.alb_tg.arn
          container_name   = "${var.service_name}-front"
          container_port   = 8085
        }
      }
      security_group_rules = {
        alb_ingress = {
          type        = "ingress"
          from_port   = 8085
          to_port     = 8085
          protocol    = "tcp"
          description = "Service port"
          cidr_blocks = ["0.0.0.0/0"]
        }
        egress_all = {
          type        = "egress"
          from_port   = 0
          to_port     = 0
          protocol    = "-1"
          cidr_blocks = ["0.0.0.0/0"]
        }
      }
      container_definitions = {
        ecs-sample = {
          name      = var.service_name
          cpu       = 512
          memory    = 1024
          essential = true
          image     = "public.ecr.aws/aws-containers/ecsdemo-frontend:776fd50"
          port_mappings = [
            {
              name          = "ecs-sample"
              containerPort = 8085
              hostPort      = 8085
              protocol      = "tcp"
            }
          ]
        }
      }
    }
    
    back_svc = {
      name                           = var.service_name
      tasks_iam_role_use_name_prefix = false
      ignore_task_definition_changes = true
      cpu                            = 512
      memory                         = 1024
      subnet_ids                     = data.aws_subnets.private.ids
      tasks_iam_role_policies = {
        secrets_manager = aws_iam_policy.secrets_manager.arn
      }
      security_group_rules = {
        alb_ingress = {
          type        = "ingress"
          from_port   = 8085
          to_port     = 8085
          protocol    = "tcp"
          description = "Service port"
          cidr_blocks = ["0.0.0.0/0"]
        }
        egress_all = {
          type        = "egress"
          from_port   = 0
          to_port     = 0
          protocol    = "-1"
          cidr_blocks = ["0.0.0.0/0"]
        }
      }
      container_definitions = {
        ecs-sample = {
          name      = var.service_name
          cpu       = 512
          memory    = 1024
          essential = true
          image     = "public.ecr.aws/aws-containers/ecsdemo-frontend:776fd50"
          port_mappings = [
            {
              name          = "ecs-sample"
              containerPort = 8085
              hostPort      = 8085
              protocol      = "tcp"
            }
          ]
        }
      }
    }
  }
  tags = {
    Terraform = "true"
  }
}
