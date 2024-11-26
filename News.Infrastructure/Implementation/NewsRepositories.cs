using Microsoft.EntityFrameworkCore;
using News.Domain.Entities;
using News.Domain.Repositories;
using News.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Infrastructure.Implementation
{
    public class NewRepositories : GenericRepository<New>,INewsRepository
    {
        private NewsDbContext _dbContext;
        public NewRepositories(NewsDbContext newsDbContext) : base(newsDbContext)
        {
            _dbContext = newsDbContext;
        }

        public void Update(New news)
        {
            var newsInDb = _dbContext.News
                .Include(n => n.Translations)
                .Include(n => n.Image)
                .FirstOrDefault(s => s.NewId == news.NewId);

            if (newsInDb != null)
            {
                // Update scalar properties
                newsInDb.IsFeatured = news.IsFeatured;
                newsInDb.CreatedDate = DateTime.Now;
                // Update translations
                newsInDb.Translations.Clear();
                foreach (var translation in news.Translations)
                {
                    newsInDb.Translations.Add(translation);
                }

                // Update images
                newsInDb.Image.Clear();
                foreach (var image in news.Image)
                {
                    newsInDb.Image.Add(image);
                }
            }
        }

    }
}
