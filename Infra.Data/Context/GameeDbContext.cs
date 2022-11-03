using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class GameeDbContext : DbContext
    {
        public GameeDbContext(DbContextOptions<GameeDbContext> options) : base(options)
        {}

        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameeDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
