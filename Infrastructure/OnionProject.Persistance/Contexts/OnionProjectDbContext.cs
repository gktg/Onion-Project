using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnionProject.Domain.Entities;

namespace OnionProject.Persistance.Contexts
{
    public class OnionProjectDbContext : DbContext
    {
        public OnionProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ErrorLogger> ErrorLogger { get; set; }
    }
}
