terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {}
}

module "vpc" {
  source               = "terraform-aws-modules/vpc/aws"
  version              = "5.15.0"
  public_subnet_names  = ["${var.vpc_name}-public", "${var.vpc_name}-public", "${var.vpc_name}-public"]
  private_subnet_names = ["${var.vpc_name}-private", "${var.vpc_name}-private", "${var.vpc_name}-private"]
  name                 = var.vpc_name
  single_nat_gateway   = true
  cidr                 = "10.0.0.0/16"

  azs             = ["sa-east-1a", "sa-east-1b", "sa-east-1c"]
  private_subnets = ["10.0.1.0/24", "10.0.2.0/24", "10.0.3.0/24"]
  public_subnets  = ["10.0.101.0/24", "10.0.102.0/24", "10.0.103.0/24"]


  enable_nat_gateway = true

  tags = {
    Terraform = "true"
  }
}