using Microsoft.EntityFrameworkCore;
using News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Infrastructure.Data
{
    public class NewsDbContext:DbContext
    {

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.News> News { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<NewsTranslation> newsTranslations { get; set; }
    }
}
