output "task_execution_role_arn" {
  value = module.ecs.task_exec_iam_role_arn
}

output "task_role_arn" {
  value = data.aws_iam_role.task_role_arn.arn
}