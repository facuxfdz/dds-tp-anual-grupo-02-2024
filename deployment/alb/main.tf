
data "aws_subnets" "public" {
  filter {
    name = "tag:Name"
    values = [var.public_subnet_name]
  }
}

data "aws_vpc" "vpc" {
  filter {
    name = "tag:Name"
    values = [var.vpc_name]
  }
}

locals {
  public_subnet_ids = data.aws_subnets.public.ids
  vpc_id = data.aws_vpc.vpc.id
}

module "acm" {
  source  = "terraform-aws-modules/acm/aws"
  version = "5.1.1"

  domain_name  = var.domain_record
  zone_id      = var.domain_zone_id
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

module "alb" {
  source = "terraform-aws-modules/alb/aws"
  version = "9.12.0"

  name    = "acceso-alimentario"
  vpc_id  = local.vpc_id
  subnets = local.public_subnet_ids

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
      certificate_arn = module.acm.acm_certificate_arn
    }
  }

  tags = {
    Environment = "Development"
    Project     = "Example"
  }
}