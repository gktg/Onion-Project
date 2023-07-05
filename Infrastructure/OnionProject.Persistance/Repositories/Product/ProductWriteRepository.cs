using OnionProject.Application.Repositories;
using OnionProject.Domain.Entities.Models;
using OnionProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Persistance.Repositories
{
    public class ProductWriteRepository : WriteRepositroy<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(OnionProjectDbContext context) : base(context)
        {
        }
    }
}
