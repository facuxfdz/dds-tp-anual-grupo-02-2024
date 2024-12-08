terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {
    key = "bastion/terraform-acceso-alimentario_releases.tfstate"
  }
}

provider "aws" {
  region = var.region
}