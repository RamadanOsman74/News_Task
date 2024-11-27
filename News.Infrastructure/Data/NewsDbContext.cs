using Microsoft.EntityFrameworkCore;
using News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Infrastructure.Data
{
    public class NewsDbContext : DbContext
    {

        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }

        public DbSet<New> News { get; set; }
        public DbSet<NewTranslation> newsTranslations { get; set; }
        public DbSet<Images> images { get; set; }

    }
}
