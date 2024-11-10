variable "public_subnet_name" {
    description = "The name of the public subnet"
    type = string
}

variable "domain_zone_id" {
    description = "The Route 53 zone ID"
    type = string
}

variable "vpc_name" {
    description = "The name of the VPC"
    type = string
}

variable "domain_record" {
    description = "The domain record"
    type = string
}

variable "alb_name" {
    description = "The name of the ALB"
    type = string
}