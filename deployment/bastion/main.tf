# Retrieve VPC details
data "aws_vpc" "selected" {
  filter {
    name   = "tag:Name"
    values = [var.vpc_name]
  }
}

# Get private subnets
data "aws_subnets" "private" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.selected.id]
  }
}

data "aws_subnet" "filtered" {
  for_each = toset(data.aws_subnets.private.ids)
  id       = each.value
}

# Filter private subnets by name
locals {
  private_subnets = [for subnet in data.aws_subnet.filtered :
    subnet.id if endswith(subnet.tags["Name"], "-private")]
}

# Security Group for Bastion
resource "aws_security_group" "bastion_sg" {
  name        = "${var.vpc_name}-bastion-sg"
  description = "Security group for bastion host"
  vpc_id      = data.aws_vpc.selected.id

  # No inbound rules needed as only SSM is used
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# IAM Role for EC2 Instance
resource "aws_iam_role" "bastion_role" {
  name               = "${var.vpc_name}-bastion-role"
  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect    = "Allow"
        Principal = { Service = "ec2.amazonaws.com" }
        Action    = "sts:AssumeRole"
      }
    ]
  })
}

resource "aws_iam_policy_attachment" "ssm_policy" {
  name       = "${var.vpc_name}-ssm-attachment"
  roles      = [aws_iam_role.bastion_role.name]
  policy_arn = "arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"
}

# Instance Profile for EC2 Instance
resource "aws_iam_instance_profile" "bastion_profile" {
  name = "${var.vpc_name}-bastion-profile"
  role = aws_iam_role.bastion_role.name
}

# EC2 Instance for Bastion
resource "aws_instance" "bastion" {
  ami           = var.ami_id
  instance_type = "t2.micro" # Free tier eligible
  subnet_id     = local.private_subnets[0]
  security_groups = [aws_security_group.bastion_sg.name]
  iam_instance_profile = aws_iam_instance_profile.bastion_profile.name
  user_data           = file("${path.module}/userdata.sh")

  tags = {
    Name = "${var.vpc_name}-bastion"
  }
}
