
using GameEntitiesBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameEntitiesBase.Repositories
{
    public class SqlEntityRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public SqlEntityRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
