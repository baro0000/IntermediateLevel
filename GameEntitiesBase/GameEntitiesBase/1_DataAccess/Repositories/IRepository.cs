using GameEntitiesBase.Data.Entities;

namespace GameEntitiesBase.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity
    {
        event EventHandler<T>? ItemAdded;
        event EventHandler<T>? ItemRemoved;
    }
}
