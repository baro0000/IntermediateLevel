using GameEntitiesBase.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameEntitiesBase.Data
{
    public class GameEntitiesBaseDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Npc> Npcs { get; set; }
        public DbSet<Statistics> Stats { get; set; }

        public GameEntitiesBaseDbContext(DbContextOptions<GameEntitiesBaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(c => c.Stats)
                .WithOne()
                .HasForeignKey<Player>(c => c.StatisticsId);

            modelBuilder.Entity<Npc>()
                .HasOne(c => c.Stats)
                .WithOne()
                .HasForeignKey<Npc>(c => c.StatisticsId);
        }
    }
}
