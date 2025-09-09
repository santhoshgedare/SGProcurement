using Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> SingleAsync(Guid id);
        Task<T?> FindAsync(Guid id); 
        Task<List<T>> ToListAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

    public interface IRootRepository<T> where T : RootEntity
    {
        Task<T> SingleAsync(Guid id);
        Task<T?> FindAsync(Guid id);
        Task<List<T>> ToListAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

}
