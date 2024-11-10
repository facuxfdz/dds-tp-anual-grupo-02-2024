variable "vpc_name" {
  description = "The name of the VPC"
  type        = string
}

variable "cluster_name" {
  description = "The name of the ECS cluster"
  type        = string
}

variable "service_name" {
  description = "The name of the ECS service"
  type        = string
}

variable "alb_name" {
  description = "The name of the ALB"
  type        = string
}

variable "region" {
  description = "The AWS region"
  type        = string
}