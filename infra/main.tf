terraform {
  required_version = ">= 1.6.0"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 6.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "~> 3.6"
    }
  }
}

provider "aws" {
  region = var.aws_region
}

data "aws_caller_identity" "current" {}

# لو عندك GitHub OIDC Provider معمول قبل كده (غالبًا موجود عندك)، الداتا دي هتشتغل.
# لو طلع Error إنه مش موجود، ساعتها اعمله مرة واحدة من AWS Console أو Terraform في مشروع منفصل.
data "aws_iam_openid_connect_provider" "github" {
  url = "https://token.actions.githubusercontent.com"
}

resource "random_id" "suffix" {
  byte_length = 3
}

locals {
  # Bucket الافتراضي بتاع Elastic Beanstalk في نفس الريجن (ده اللي هنرفع عليه zip من GitHub Actions)
  eb_bucket = "elasticbeanstalk-${var.aws_region}-${data.aws_caller_identity.current.account_id}"
}

############################
# Variables
############################
variable "aws_region" {
  type    = string
  default = "eu-central-1"
}

variable "app_name" {
  type    = string
  default = "codeikoo-backend"
}

variable "env_name" {
  type    = string
  default = "codeikoo-backend-prod"
}

variable "instance_type" {
  type    = string
  default = "t3.micro"
}

variable "environment_type" {
  type    = string
  default = "SingleInstance" # أو LoadBalanced
}

# GitHub Repo اللي هيعمل Deploy
variable "github_owner" {
  type    = string
  default = "ASV47"
}

variable "github_repo" {
  type    = string
  default = "PUT-YOUR-BACKEND-REPO-NAME-HERE"
}

variable "github_branch" {
  type    = string
  default = "main"
}

# منصة EB (لو Laravel/PHP خليها زي ما هي)
# لو Node/.NET غير الـ regex حسب المنصة.
variable "solution_stack_regex" {
  type    = string
  default = "64bit Amazon Linux 2023.*running PHP 8\\."
}

############################
# Elastic Beanstalk App / Env
############################
resource "aws_elastic_beanstalk_application" "app" {
  name = var.app_name
}

data "aws_elastic_beanstalk_solution_stack" "stack" {
  most_recent = true
  name_regex  = var.solution_stack_regex
}

# Service Role for Elastic Beanstalk
data "aws_iam_policy_document" "eb_service_assume" {
  statement {
    actions = ["sts:AssumeRole"]
    principals {
      type        = "Service"
      identifiers = ["elasticbeanstalk.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "eb_service_role" {
  name               = "${var.app_name}-eb-service-role"
  assume_role_policy = data.aws_iam_policy_document.eb_service_assume.json
}

# AWS بتوصي بسياسات EnhancedHealth + ManagedUpdates للـ service role
resource "aws_iam_role_policy_attachment" "eb_service_health" {
  role       = aws_iam_role.eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSElasticBeanstalkEnhancedHealth"
}

resource "aws_iam_role_policy_attachment" "eb_service_managed_updates" {
  role       = aws_iam_role.eb_service_role.name
  policy_arn = "arn:aws:iam::aws:policy/AWSElasticBeanstalkManagedUpdatesCustomerRolePolicy"
}

# EC2 Instance Role (للـ instance اللي بيشغل الـ app)
data "aws_iam_policy_document" "eb_ec2_assume" {
  statement {
    actions = ["sts:AssumeRole"]
    principals {
      type        = "Service"
      identifiers = ["ec2.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "eb_ec2_role" {
  name               = "${var.app_name}-eb-ec2-role"
  assume_role_policy = data.aws_iam_policy_document.eb_ec2_assume.json
}

resource "aws_iam_role_policy_attachment" "eb_webtier" {
  role       = aws_iam_role.eb_ec2_role.name
  policy_arn = "arn:aws:iam::aws:policy/AWSElasticBeanstalkWebTier"
}

resource "aws_iam_role_policy_attachment" "eb_ssm" {
  role       = aws_iam_role.eb_ec2_role.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"
}

resource "aws_iam_instance_profile" "eb_instance_profile" {
  name = "${var.app_name}-eb-instance-profile"
  role = aws_iam_role.eb_ec2_role.name
}

resource "aws_elastic_beanstalk_environment" "env" {
  name                = var.env_name
  application         = aws_elastic_beanstalk_application.app.name
  solution_stack_name = data.aws_elastic_beanstalk_solution_stack.stack.name

  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "EnvironmentType"
    value     = var.environment_type
  }

  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "ServiceRole"
    value     = aws_iam_role.eb_service_role.name
  }

  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "IamInstanceProfile"
    value     = aws_iam_instance_profile.eb_instance_profile.name
  }

  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "InstanceType"
    value     = var.instance_type
  }

  # لو Laravel/PHP: خلي الـ document root = /public
  setting {
    namespace = "aws:elasticbeanstalk:container:php:phpini"
    name      = "document_root"
    value     = "/public"
  }
}

############################
# GitHub Actions Deploy Role (OIDC)
############################
data "aws_iam_policy_document" "gha_assume" {
  statement {
    actions = ["sts:AssumeRoleWithWebIdentity"]
    principals {
      type        = "Federated"
      identifiers = [data.aws_iam_openid_connect_provider.github.arn]
    }

    condition {
      test     = "StringEquals"
      variable = "token.actions.githubusercontent.com:aud"
      values   = ["sts.amazonaws.com"]
    }

    # يقفلها على ريبو + برانش محدد
    condition {
      test     = "StringLike"
      variable = "token.actions.githubusercontent.com:sub"
      values   = ["repo:${var.github_owner}/${var.github_repo}:ref:refs/heads/${var.github_branch}"]
    }
  }
}

resource "aws_iam_role" "gha_deploy" {
  name               = "${var.app_name}-gha-deploy"
  assume_role_policy = data.aws_iam_policy_document.gha_assume.json
}

# أسهل/أضمن صلاحيات عشان تبطل AccessDenied:
# - AdministratorAccess-AWSElasticBeanstalk بدل AWSElasticBeanstalkFullAccess (القديم اتبدل)
# - AmazonS3FullAccess
resource "aws_iam_role_policy_attachment" "gha_eb_admin" {
  role       = aws_iam_role.gha_deploy.name
  policy_arn = "arn:aws:iam::aws:policy/AdministratorAccess-AWSElasticBeanstalk"
}

resource "aws_iam_role_policy_attachment" "gha_s3_full" {
  role       = aws_iam_role.gha_deploy.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonS3FullAccess"
}

############################
# Outputs
############################
output "app_url" {
  value = "http://${aws_elastic_beanstalk_environment.env.cname}"
}

output "eb_app_name" {
  value = aws_elastic_beanstalk_application.app.name
}

output "eb_env_name" {
  value = aws_elastic_beanstalk_environment.env.name
}

output "eb_bucket" {
  value = local.eb_bucket
}

output "gha_role_arn" {
  value = aws_iam_role.gha_deploy.arn
}
