module "ecr" {
  source  = "terraform-aws-modules/ecr/aws"
  version = "2.3.0"

  repository_name = var.repository_name

  repository_lifecycle_policy = templatefile(var.lifecycle_policy_file, {})

  tags = {
    Terraform = "true"
  }
}

module "ecr_front" {
  source  = "terraform-aws-modules/ecr/aws"
  version = "2.3.0"

  repository_name = "${var.repository_name}/front"

  repository_lifecycle_policy = templatefile(var.lifecycle_policy_file, {})

  tags = {
    Terraform = "true"
  }
}

module "ecr_recomendaciones_api" {
  source  = "terraform-aws-modules/ecr/aws"
  version = "2.3.0"

  repository_name = "${var.repository_name}/recomendaciones-api"

  repository_lifecycle_policy = templatefile(var.lifecycle_policy_file, {})

  tags = {
    Terraform = "true"
  }
}