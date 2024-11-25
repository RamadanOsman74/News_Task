using Microsoft.EntityFrameworkCore;
using News.Domain.Repositories;
using News.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Infrastructure.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly NewsDbContext _DbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(NewsDbContext newsDbContext)
        {
            _DbContext = newsDbContext;
            _dbSet = _DbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity); 
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault()
        {
            IQueryable<T> query = _dbSet;
            return query.SingleOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }


    }
}
