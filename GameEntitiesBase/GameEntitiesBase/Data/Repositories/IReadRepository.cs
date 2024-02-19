using GameEntitiesBase.Data.Entities;

namespace GameEntitiesBase.Data.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        int CountObjects();
        void SaveToFile(string path);
    }
}
