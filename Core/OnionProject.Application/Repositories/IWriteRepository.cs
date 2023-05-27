using OnionProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAysnc(T model);
        Task<bool> AddRangeAysnc(List<T> model);
        bool Remove(T model);
        Task<bool> RemoveById(Guid id);
        bool Update(T model);
        Task<int> SaveAsync();

    }
}
