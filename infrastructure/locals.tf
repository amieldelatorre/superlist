locals {
  tags = {
    "TerraformProject" = "https://github.com/amieldelatorre/vlist/infrastructure"
  }

  private_subnets = (
    {
      "a" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.private_a
      },
      "b" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.private_b
      },
      "c" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.private_c
      },
    }
  )

  public_subnets = (
    {
      "a" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.public_a
      },
      "b" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.public_b
      },
      "c" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.public_c
      },
    }
  )

  db_subnets = (
    {
      "a" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.db_a
      },
      "b" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.db_b
      },
      "c" = {
        "cidr_block" = module.subnet_addrs.network_cidr_blocks.db_c
      },
    }
  )

  nat_gateways = ({
    "a" = 1,
    "b" = 0,
    "c" = 0,
  })
}