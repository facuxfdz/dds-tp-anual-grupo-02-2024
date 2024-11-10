resource "random_password" "master_passwd" {
    length           = 16
    special          = true
    override_special = "!#$%&*()-_=+[]{}<>:?"
}

resource "aws_secretsmanager_secret" "db_secret"{
    name = var.db_identifier
    description = "Master password for RDS instance"   
}

resource "aws_secretsmanager_secret_version" "db_secret_version"{
    secret_id = aws_secretsmanager_secret.db_secret.id
    secret_string = random_password.master_passwd.result
}

module "db" {
  source  = "terraform-aws-modules/rds/aws"
  version = "6.10.0"
  
  identifier = var.db_identifier

  # All available versions: http://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/CHAP_MySQL.html#MySQL.Concepts.VersionMgmt
  engine               = "mysql"
  engine_version       = "8.0"
  family               = "mysql8.0" # DB parameter group
  major_engine_version = "8.0"      # DB option group
  instance_class       = "db.t3.micro"

  allocated_storage     = 20
  max_allocated_storage = 40

  db_name  = var.db_name
  username = var.db_username
  manage_master_user_password = false
  password = random_password.master_passwd.result
  port     = 3306

  multi_az               = false
  create_db_subnet_group = true
  

  skip_final_snapshot = true
  deletion_protection = false
  
}