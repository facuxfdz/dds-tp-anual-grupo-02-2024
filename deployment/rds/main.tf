resource "random_password" "master_passwd" {
  length           = 16
  special          = true
  override_special = "!#$%&*()-_=+[]{}<>:?"
}

resource "aws_secretsmanager_secret" "db_secret" {
  name_prefix = "acceso_alimentario/db_connection_data"
  description = "Master password for RDS instance"
}

// Store the secret name in an SSM parameter
resource "aws_ssm_parameter" "db_secret_name" {
  name  = "/AccesoAlimentario/Production/DB/SecretName"
  type  = "String"
  value = aws_secretsmanager_secret.db_secret.name
}

# We need to remove the ":3306" from the endpoint module.db.db_instance_endpoint
resource "aws_secretsmanager_secret_version" "db_secret_version" {
  secret_id = aws_secretsmanager_secret.db_secret.id
  secret_string = jsonencode({
    DB_SERVER   = replace(module.db.db_instance_endpoint, ":3306", ""),
    DB_NAME     = var.db_name,
    DB_USERNAME = var.db_username,
    DB_PASSWORD = random_password.master_passwd.result
  })
}

data "aws_subnets" "private" {
  filter {
    name   = "tag:Name"
    values = ["${var.vpc_name}-private"]
  }
}

data "aws_vpc" "selected" {
  filter {
    name   = "tag:Name"
    values = [var.vpc_name]
  }
}

resource "aws_security_group" "db" {
  name        = "${var.db_identifier}-sg"
  description = "Security group for RDS instance"
  vpc_id      = data.aws_vpc.selected.id

  ingress {
    from_port   = 3306
    to_port     = 3306
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

module "db" {
  source  = "terraform-aws-modules/rds/aws"
  version = "6.10.0"

  identifier = var.db_identifier

  # All available versions: http://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/CHAP_MySQL.html#MySQL.Concepts.VersionMgmt
  engine                 = "mysql"
  engine_version         = "8.0"
  family                 = "mysql8.0" # DB parameter group
  major_engine_version   = "8.0"      # DB option group
  instance_class         = "db.t3.micro"
  subnet_ids             = data.aws_subnets.private.ids
  allocated_storage      = 20
  max_allocated_storage  = 40
  vpc_security_group_ids = [aws_security_group.db.id]

  db_name                     = var.db_name
  username                    = var.db_username
  manage_master_user_password = false
  password                    = random_password.master_passwd.result
  port                        = 3306

  multi_az               = false
  create_db_subnet_group = true


  skip_final_snapshot = true
  deletion_protection = false

}