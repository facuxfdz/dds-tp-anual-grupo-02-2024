
terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {}
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
      ignore_task_definition_changes = true
      cpu    = 512
      memory = 1024
      subnet_ids = data.aws_subnets.private.ids
      
      # Container definition(s)
      container_definitions = {

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
      }
    }
  }

  tags = {
    Environment = "Development"
    Project     = "Example"
  }
}