using GameEntitiesBase.Data.Entities.Extensions;
using GameEntitiesBase.Data.Entities;
using System.Text.Json;

namespace GameEntitiesBase.Data.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class, IEntity, IEntityWithStatistics, new()
    {
        private List<T> list = new List<T>();

        public ListRepository()
        {
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return list;
        }
        public void SaveToFile(string path) { }
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
                        list.Add(obj);
                        line = reader.ReadLine();
                    }
                }
            }
        }
        public T? GetById(int id)
        {
            return list.Find(x => x.Id == id);
        }
        public int CountObjects()
        {
            return list.Count();
        }

        public void Add(T item) { }
        public void Remove(T item) { }
        public void Save() { }
    }
}
