using GameEntitiesBase.Entities;
using GameEntitiesBase.Entities.Extensions;
using System.Text.Json;

namespace GameEntitiesBase.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddBatch<T>(this IRepository<T> repository, List<T> collection)
            where T : class, IEntity, new()
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
        public static void SaveToFile<T>(this IRepository<T> repository, string path) where T : class, IEntity, new()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (var writer = File.AppendText(path))
            {
                foreach (var obj in repository.GetAll())
                {
                    writer.WriteLine(JsonSerializer.Serialize(obj));
                }
            }
        }
        public static void LoadFromFile<T>(this IRepository<T> repository, string path) where T : class, IEntity, new()
        {
            if (File.Exists(path))
            {
                using (var reader = File.OpenText(path))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var obj = JsonSerializer.Deserialize<T>(line);
                        repository.Add(obj);
                        line = reader.ReadLine();
                    }
                    repository.Save();
                }
            }
        }
    }
}
