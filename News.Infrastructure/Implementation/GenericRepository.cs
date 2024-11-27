using Microsoft.EntityFrameworkCore;
using News.Domain.Repositories;
using News.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return query.ToList();
        }

        public T Get(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault(e => EF.Property<int>(e, "NewId") == id); 
        }


        public void Remove(T entity)
            {
                _dbSet.Remove(entity);
            }

        }
    }

