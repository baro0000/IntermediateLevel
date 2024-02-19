using GameEntitiesBase.Data.Entities;

namespace GameEntitiesBase.Data.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Save();
        void LoadFromFile(string path);
    }
}
