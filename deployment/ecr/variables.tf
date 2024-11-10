variable "lifecycle_policy_file" {
  description = "Path to the file containing the lifecycle policy"
  type        = string
}

variable "repository_name" {
  description = "The name of the ECR repository"
  type        = string
}

variable "region" {
  description = "The AWS region"
  type        = string
}