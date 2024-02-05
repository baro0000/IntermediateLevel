namespace GameEntitiesBase.Entities
{
    public class GameMaster : EntityBase
    {
        public string Name { get; set; }

        public GameMaster()
        {
            
        }

        public GameMaster(string name)
        {
            Name = name;
        }

        public override string ToString() => $"Id: {Id} Name: {Name} (GM)";
    }
}
