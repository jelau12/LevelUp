using LevelUp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LevelUp.Entities.Models.Context
{
    public class LevelUpDbContext : DbContext
    {
        public LevelUpDbContext()
        {

        }
        public LevelUpDbContext(DbContextOptions<LevelUpDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=DESKTOP-K97UKIM; initial catalog=LevelUp;integrated security=True;multipleActiveResultSets=True;");
            }
        }
    }
}