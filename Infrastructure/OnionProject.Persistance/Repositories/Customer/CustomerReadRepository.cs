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
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(OnionProjectDbContext context) : base(context)
        {
        }
    }
}
