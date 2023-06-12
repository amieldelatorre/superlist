variable "region" {
  type        = string
  description = "Region to create resources in" 
}

variable "aws_profile" {
  type        = string
  description = "AWS profile to use"
}

variable "vpc_cidr_block" {
  type        = string
  description = "The cidr block to use for the vpc"
}