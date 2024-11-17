variable "public_subnet_name" {
  description = "The name of the public subnet"
  type        = string
}

variable "domain_zone_id" {
  description = "The Route 53 zone ID"
  type        = string
}

variable "vpc_name" {
  description = "The name of the VPC"
  type        = string
}

variable "domain_record" {
  description = "The domain record"
  type        = string
}

variable "alb_name" {
  description = "The name of the ALB"
  type        = string
}

variable "region" {
  description = "The region"
  type        = string
}

# Cognito
variable "user_pool_arn" {
  description = "The ARN of the Cognito user pool"
  type        = string
}

variable "user_pool_client_id" {
  description = "The client ID of the Cognito user pool"
  type        = string
}

variable "user_pool_domain" {
  description = "The domain of the Cognito user pool"
  type        = string
}