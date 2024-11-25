using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Domain.Repositories
{
    // class to any crud Operations  
    public interface IGenericRepository<T> where  T : class
    {
        IEnumerable<T> GetAll();
        T GetFirstOrDefault();
        void Add(T entity);
        void Remove(T entity); 

    }
}
