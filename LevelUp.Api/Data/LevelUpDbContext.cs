using Microsoft.EntityFrameworkCore;

namespace LevelUp.Api.Data
{
    public class LevelUpDbContext : DbContext
    {
        public LevelUpDbContext(DbContextOptions<LevelUpDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
