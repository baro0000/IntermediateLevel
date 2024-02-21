using GameEntitiesBase.Data.Entities;
using GameEntitiesBase.Data.Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GameEntitiesBase.Data.Repositories
{
    public class SqlEntityRepository<T> : IRepository<T> where T : class, IEntity, IEntityWithStatistics, new()
    {
        private readonly GameEntitiesBaseDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public SqlEntityRepository(GameEntitiesBaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _dbSet = _dbContext.Set<T>();
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return _dbSet.Include(p => p.Stats).ToList();
        }
        public void SaveToFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (var writer = File.AppendText(path))
            {
                foreach (var obj in GetAll())
                {
                    writer.WriteLine(JsonSerializer.Serialize(obj));
                }
            }
        }
        public void LoadFromFile(string path)
        {
            if (File.Exists(path))
            {
                using (var reader = File.OpenText(path))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var obj = JsonSerializer.Deserialize<T>(line);
                        _dbSet.Add(obj);
                        line = reader.ReadLine();
                    }
                    _dbContext.SaveChanges();
                }
            }
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public int CountObjects()
        {
            return _dbSet.Count();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
