using GameEntitiesBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameEntitiesBase.Data
{
    public class GameEntitiesBaseDbContext : DbContext
    {
        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameMaster> GameMasters => Set<GameMaster>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
