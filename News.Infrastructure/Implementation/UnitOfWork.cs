using News.Domain.Repositories;
using News.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private NewsDbContext _dbContext;
        public INewsRepository News { get; private set; }

        public UnitOfWork(NewsDbContext newsDbContext) 
        {
            _dbContext = newsDbContext;
            News = new NewRepositories(newsDbContext);
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
