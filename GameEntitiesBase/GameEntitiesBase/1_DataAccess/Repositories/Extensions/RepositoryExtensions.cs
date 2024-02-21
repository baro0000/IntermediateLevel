using GameEntitiesBase.Data.Entities;
using GameEntitiesBase.Data.Repositories;
using System.Text.Json;

namespace GameEntitiesBase.Data.Repositories.Extensions
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
