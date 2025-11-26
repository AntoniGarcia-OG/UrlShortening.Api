using Microsoft.EntityFrameworkCore;
using UrlShortening.Api.Models;

namespace UrlShortening.Api.Data
{
    public class UrlShorteningDbContext(DbContextOptions<UrlShorteningDbContext> options) : DbContext(options)
    {
        public DbSet<UrlMapping> UrlMappings { get; set; }
    }
}
