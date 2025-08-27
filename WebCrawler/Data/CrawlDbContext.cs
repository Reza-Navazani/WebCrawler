// Data/CrawlDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebCrawler.Api.Data
{
    public class CrawlDbContext : DbContext
    {
        public CrawlDbContext(DbContextOptions<CrawlDbContext> options) : base(options) { }

        public DbSet<PageResult> Pages { get; set; }
    }
}
