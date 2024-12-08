variable "vpc_name" {
  description = "Name of the VPC to look up"
  type        = string
}

variable "ami_id" {
  description = "AMI ID for the EC2 instance"
  type        = string
}

variable "region" {
  description = "AWS Region to deploy resources"
  type        = string
}
