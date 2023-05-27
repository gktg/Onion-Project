﻿using OnionProject.Application.Repositories;
using OnionProject.Domain.Entities;
using OnionProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Persistance.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(OnionProjectDbContext context) : base(context)
        {
        }
    }
}
