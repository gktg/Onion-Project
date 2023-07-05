using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProject.Domain.Entities.Models;

namespace OnionProject.Application.Repositories
{
    public interface ICustomerWriteRepository : IWriteRepository<Customer>
    {
    }
}
