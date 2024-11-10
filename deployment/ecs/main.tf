terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {
    region = var.region
    key     = "ecs/terraform-acceso-alimentario_releases.tfstate"
  }
}

provider "aws" {
    region = var.region
}
data "aws_lb_target_group" "alb_tg" {
  name = var.alb_name
}
data "aws_subnets" "private" {
  filter {
    name = "tag:Name"
    values = ["${var.vpc_name}-private"]
  }
}

module "ecs" {
  source = "terraform-aws-modules/ecs/aws"
  version = "5.11.4"
  
  cluster_name = var.cluster_name

  create_task_exec_iam_role = true
  create_task_exec_policy = true
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
    initial_svc = {
      name = var.service_name
      tasks_iam_role_name = "${var.service_name}-task-role" 
      ignore_task_definition_changes = true
      cpu    = 512
      memory = 1024
      subnet_ids = data.aws_subnets.private.ids
      load_balancer = {
        service = {
          target_group_arn = data.aws_lb_target_group.alb_tg.arn
          container_name   = var.service_name
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
          type      = "egress"
          from_port = 0
          to_port   = 0
          protocol  = "-1"
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
