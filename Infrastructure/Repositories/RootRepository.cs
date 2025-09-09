using Core.Entities.Common;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RootRepository<T> : IRootRepository<T> where T : RootEntity
    {
        protected readonly SGPContext _context;
        protected readonly DbSet<T> _dbSet;

        public RootRepository(SGPContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> SingleAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(T).Name} with Id {id} not found.");
            return entity;
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }

        public async Task<List<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
