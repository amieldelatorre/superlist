resource "aws_vpc" "this" {
  cidr_block = var.vpc_cidr_block
  
  tags       = merge(
    local.tags,
    {
      "Name" = "vList VPC"
    }
  )    
}

resource "aws_internet_gateway" "this" {
  vpc_id = aws_vpc.this.id

  tags   = merge(
    local.tags,
    {
      "Name" = "vList VPC"
    }
  )
}

resource "aws_subnet" "private" {
  for_each          = local.private_subnets
  vpc_id            = aws_vpc.this.id
  cidr_block        = each.value.cidr_block
  availability_zone = "${var.region}${each.key}"

  tags              = merge(
    local.tags, 
    {
      Name = "vList-private-${each.key}"
    }
  )
}

resource "aws_subnet" "public" {
  for_each          = local.public_subnets
  vpc_id            = aws_vpc.this.id
  cidr_block        = each.value.cidr_block
  availability_zone = "${var.region}${each.key}"

  tags              = merge(
    local.tags, 
    {
      Name = "vList-public-${each.key}"
    }
  )
}

resource "aws_subnet" "db" {
  for_each          = local.db_subnets
  vpc_id            = aws_vpc.this.id
  cidr_block        = each.value.cidr_block
  availability_zone = "${var.region}${each.key}"

  tags              = merge(
    local.tags, 
    {
      Name = "vList-db-${each.key}"
    }
  )
}

resource "aws_eip" "nat-gateway-a" {
  count  = local.nat_gateways["a"]
  domain = "vpc"

  depends_on = [ aws_internet_gateway.this ]
  
  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-eip-a"
    }
  )
}

resource "aws_eip" "nat-gateway-b" {
  count  = local.nat_gateways["b"]
  domain = "vpc"

  depends_on = [ aws_internet_gateway.this ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-eip-b"
    }
  )
}

resource "aws_eip" "nat-gateway-c" {
  count  = local.nat_gateways["c"]
  domain = "vpc"

  depends_on = [ aws_internet_gateway.this ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-eip-c"
    }
  )
}

resource "aws_nat_gateway" "nat_gateway_a" {
  count         = local.nat_gateways["a"]
  allocation_id = aws_eip.nat-gateway-a[0].id
  subnet_id     = aws_subnet.public["a"].id

  depends_on = [ aws_subnet.public["a"], aws_eip.nat-gateway-a ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-a"
    }
  )
}

resource "aws_nat_gateway" "nat_gateway_b" {
  count         = local.nat_gateways["b"]
  allocation_id = aws_eip.nat-gateway-b[0].id
  subnet_id     = aws_subnet.public["b"].id

  depends_on = [ aws_subnet.public["b"], aws_eip.nat-gateway-b ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-b"
    }
  )
}

resource "aws_nat_gateway" "nat_gateway_c" {
  count         = local.nat_gateways["c"]
  allocation_id = aws_eip.nat-gateway-c[0].id
  subnet_id     = aws_subnet.public["c"].id

  depends_on = [ aws_subnet.public["c"], aws_eip.nat-gateway-c ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-nat-gateway-c"
    }
  )
}

resource "aws_route_table" "public" {
  vpc_id = aws_vpc.this.id

  depends_on = [ aws_internet_gateway.this ]

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.this.id
  }

  tags = merge(
    local.tags, 
    {
      Name = "vList-public"
    }
  )
}

resource "aws_route_table_association" "public" {
  for_each = aws_subnet.public
  subnet_id = each.value.id
  route_table_id = aws_route_table.public.id
  depends_on = [ aws_route_table.public ]
}

resource "aws_route_table" "db" {
  vpc_id = aws_vpc.this.id

  depends_on = [ aws_internet_gateway.this ]

  tags = merge(
    local.tags, 
    {
      Name = "vList-db"
    }
  )
}

resource "aws_route_table_association" "db" {
  for_each = aws_subnet.db
  subnet_id = each.value.id
  route_table_id = aws_route_table.db.id
  depends_on = [ aws_route_table.db ]
}

resource "aws_route_table" "private_a" {
  count = local.nat_gateways["a"]
  vpc_id = aws_vpc.this.id

  depends_on = [ aws_nat_gateway.nat_gateway_a ]

  route {
    cidr_block = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat_gateway_a[0].id
  }

  tags = merge(
    local.tags, 
    {
      Name = "vList-private-a"
    }
  )
}

resource "aws_route_table_association" "private_a" {
  count = local.nat_gateways["a"]
  subnet_id = aws_subnet.private["a"].id
  route_table_id = aws_route_table.private_a[0].id
  depends_on = [ aws_route_table.private_a ]
}

resource "aws_route_table" "private_b" {
  count = local.nat_gateways["b"]
  vpc_id = aws_vpc.this.id

  depends_on = [ aws_nat_gateway.nat_gateway_b ]

  route {
    cidr_block = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat_gateway_b[0].id
  }

  tags = merge(
    local.tags, 
    {
      Name = "vList-private-b"
    }
  )
}

resource "aws_route_table_association" "private_b" {
  count = local.nat_gateways["b"]
  subnet_id = aws_subnet.private["b"].id
  route_table_id = aws_route_table.private_b[0].id
  depends_on = [ aws_route_table.private_b ]
}

resource "aws_route_table" "private_c" {
  count = local.nat_gateways["c"]
  vpc_id = aws_vpc.this.id

  depends_on = [ aws_nat_gateway.nat_gateway_c ]

  route {
    cidr_block = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat_gateway_c[0].id
  }

  tags = merge(
    local.tags, 
    {
      Name = "vList-private-c"
    }
  )
}

resource "aws_route_table_association" "private_c" {
  count = local.nat_gateways["c"]
  subnet_id = aws_subnet.private["c"].id
  route_table_id = aws_route_table.private_c[0].id
  depends_on = [ aws_route_table.private_c ]
}

module "subnet_addrs" {
  source = "hashicorp/subnets/cidr"

  base_cidr_block = aws_vpc.this.cidr_block
  networks = [
    {
      name     = "public_a"
      new_bits = 8
    },
    {
      name     = "public_b"
      new_bits = 8
    },
    {
      name     = "public_c"
      new_bits = 4
    },
    {
      name     = "private_a"
      new_bits = 8
    },
    {
      name     = "private_b"
      new_bits = 8
    },
    {
      name     = "private_c"
      new_bits = 8
    },
    {
      name     = "db_a"
      new_bits = 8
    },
    {
      name     = "db_b"
      new_bits = 8
    },
    {
      name     = "db_c"
      new_bits = 8
    },
  ]
}

