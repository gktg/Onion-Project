using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnionProject.Application.Repositories;
using OnionProject.Domain.Entities.Common;
using OnionProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Persistance.Repositories
{
    public class WriteRepositroy<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly OnionProjectDbContext _context;

        public WriteRepositroy(OnionProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public bool Add(T model)
        {
            EntityEntry<T> entityEntry = Table.Add(model);

            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddAysnc(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);

            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAysnc(List<T> model)
        {
            await Table.AddRangeAsync(model);

            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);

            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> RemoveById(Guid id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == id);

            return Remove(model);
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);

            return entityEntry.State == EntityState.Added;
        }
        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
