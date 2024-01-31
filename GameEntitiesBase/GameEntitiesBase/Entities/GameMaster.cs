namespace GameEntitiesBase.Entities
{
    public class GameMaster : EntityBase
    {
        public string Name { get; set; }

        public override string ToString() => $"Id: {Id} Name: {Name} (GM)";
    }
}
