variable "vpc_name" {
  description = "The name of the VPC"
  type        = string
}
variable "db_identifier" {
  description = "The identifier for the DB instance"
  type        = string
}

variable "db_name" {
  description = "The name for the default database created in RDS instance"
  type        = string
}

variable "db_username" {
  description = "The master username for the database"
  type        = string
}

variable "region" {
  description = "The AWS region"
  type        = string
}