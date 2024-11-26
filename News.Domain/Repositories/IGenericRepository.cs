using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Domain.Repositories
{
    // class to any crud Operations  
    public interface IGenericRepository<T> where  T : class
    {
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> include = null);
        public T Get(int id, Func<IQueryable<T>, IQueryable<T>> include = null);
        void Add(T entity);
        void Remove(T entity); 

    }
}
