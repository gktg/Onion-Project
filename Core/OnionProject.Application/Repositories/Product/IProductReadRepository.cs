using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProject.Domain.Entities;

namespace OnionProject.Application.Repositories
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
    }
}
