data "aws_subnets" "public" {
  filter {
    name   = "tag:Name"
    values = [var.public_subnet_name]
  }
}

data "aws_vpc" "vpc" {
  filter {
    name   = "tag:Name"
    values = [var.vpc_name]
  }
}

locals {
  public_subnet_ids = data.aws_subnets.public.ids
  vpc_id            = data.aws_vpc.vpc.id
}

module "acm" {
  source  = "terraform-aws-modules/acm/aws"
  version = "5.1.1"

  domain_name       = "*.${var.domain_record}"
  zone_id           = var.domain_zone_id
  validation_method = "DNS"

  wait_for_validation = true
}

module "root_acm" {
  source  = "terraform-aws-modules/acm/aws"
  version = "5.1.1"

  domain_name       = var.domain_record
  zone_id           = var.domain_zone_id
  validation_method = "DNS"

  wait_for_validation = true
}

# Route53 record pointing to the ALB
resource "aws_route53_record" "acceso_alimentario" {
  zone_id = var.domain_zone_id
  name    = var.domain_record
  type    = "A"

  alias {
    name                   = module.alb.dns_name
    zone_id                = module.alb.zone_id
    evaluate_target_health = false
  }
}

# Route53 additional records pointing to the ALB
resource "aws_route53_record" "back_acceso_alimentario" {
  for_each = toset(var.additional_records)
  zone_id  = var.domain_zone_id
  name     = each.value
  type     = "A"

  alias {
    name                   = module.alb.dns_name
    zone_id                = module.alb.zone_id
    evaluate_target_health = false
  }
}
module "alb" {
  source  = "terraform-aws-modules/alb/aws"
  version = "9.12.0"

  enable_deletion_protection = false
  name                       = var.alb_name
  vpc_id                     = local.vpc_id
  subnets                    = local.public_subnet_ids

  # Security Group
  security_group_ingress_rules = {
    all_http = {
      from_port   = 80
      to_port     = 80
      ip_protocol = "tcp"
      description = "HTTP web traffic"
      cidr_ipv4   = "0.0.0.0/0"
    }
    all_https = {
      from_port   = 443
      to_port     = 443
      ip_protocol = "tcp"
      description = "HTTPS web traffic"
      cidr_ipv4   = "0.0.0.0/0"
    }
  }
  security_group_egress_rules = {
    all = {
      ip_protocol = "-1"
      cidr_ipv4   = "0.0.0.0/0"
    }
  }

  listeners = {
    ex-http-https-redirect = {
      port     = 80
      protocol = "HTTP"
      redirect = {
        port        = "443"
        protocol    = "HTTPS"
        status_code = "HTTP_301"
      }
    }
    ex-https = {
      port            = 443
      protocol        = "HTTPS"
      certificate_arn = module.root_acm.acm_certificate_arn
      additional_certificate_arns = [
        module.acm.acm_certificate_arn
      ]

      # fixed response 404
      fixed_response = {
        content_type = "text/plain"
        message_body = "404: Not Found"
        status_code  = "404"
      }
      # forward = {
      #   target_group_key = "ex-tg"
      # }
    }
  }

  # target_groups = {
  #   ex-tg = {
  #     name              = var.alb_name
  #     create_attachment = false
  #     protocol          = "HTTP"
  #     port              = 8085
  #     target_type       = "ip"
  #     health_check = {
  #       path                = "/"
  #       protocol            = "HTTP"
  #       matcher             = "200-399"
  #       interval            = 30
  #       timeout             = 5
  #       healthy_threshold   = 2
  #       unhealthy_threshold = 2
  #     }
  #   }
  # }

  tags = {
    Environment = "Development"
    Project     = "Example"
  }
}