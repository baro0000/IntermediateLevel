using GameEntitiesBase.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json;

namespace GameEntitiesBase.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly List<T> _items = new();

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
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
                        T? obj = JsonSerializer.Deserialize<T>(line);
                        _items.Add(obj!);
                        line = reader.ReadLine();
                    }
                }
            }
        }
        public T GetById(int id) => _items.SingleOrDefault(item => item.Id == id);
        public int Count()
        {
            return _items.Count;
        }
        public void Add(T item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            //Not needed
        }
    }
}
