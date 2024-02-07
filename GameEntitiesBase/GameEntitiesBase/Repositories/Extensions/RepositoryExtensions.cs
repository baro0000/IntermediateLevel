using GameEntitiesBase.Entities;
using GameEntitiesBase.Entities.Extensions;

namespace GameEntitiesBase.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddBatch<T>(this IRepository<T> repository, List<T> collection) 
            where T : class, IEntity, new()
        {
            foreach (var item in collection)
            {
                repository.Add(item);
            }
            repository.Save();
        }

        public static void AddBatch<T>(this IWriteRepository<T> repository, List<T> collection)
            where T : class, IEntity, new()
        {
            foreach (var item in collection)
            {
                repository.Add(item);
            }
            repository.Save();
        }
    }
}
