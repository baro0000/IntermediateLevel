using GameEntitiesBase.Entities;
using GameEntitiesBase.Entities.Extensions;
using System.Text.Json;

namespace GameEntitiesBase.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddBatch<T>(this IRepository<T> repository, List<T> collection)
            where T : class, IEntity
        {
            if (repository != null)
            {
                foreach (var item in collection)
                {
                    repository.Add(item);
                }
                repository.Save();
            }
        }
    }
}
