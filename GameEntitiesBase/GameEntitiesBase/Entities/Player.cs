namespace GameEntitiesBase.Entities
{
    public class Player : EntityBase
    {
        public string Name { get; set; }

        public override string ToString() => $"Id: {Id} Name: {Name}";
    }
}
