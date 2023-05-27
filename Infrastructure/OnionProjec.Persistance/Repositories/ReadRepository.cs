using Microsoft.EntityFrameworkCore;
using OnionProject.Application.Repositories;
using OnionProject.Domain.Entities.Common;
using OnionProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly OnionProjectDbContext _context;

        public ReadRepository(OnionProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method);
        }

        public async Task<T> GetSingleAysnc(Expression<Func<T, bool>> method)
        {
            return await Table.FirstAsync(method);
        }

        public async Task<T> GetByIdAysnc(Guid id)
        {
            return await Table.FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
