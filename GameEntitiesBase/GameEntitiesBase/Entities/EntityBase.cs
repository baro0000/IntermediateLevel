using System.Xml.Linq;

namespace GameEntitiesBase.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
    }
}
