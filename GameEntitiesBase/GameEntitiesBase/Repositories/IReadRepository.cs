using GameEntitiesBase.Entities;

namespace GameEntitiesBase.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        int Count();
        void SaveToFile(string path);
    }
}
