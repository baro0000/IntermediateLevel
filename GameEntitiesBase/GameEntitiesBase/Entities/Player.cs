namespace GameEntitiesBase.Entities
{
    public class Player : EntityBase
    {
        public Player()
        {
            
        }
        public Player(string name)
        {
            Name = name;
        }

        public override string ToString() => $"Id: {Id} Name: {Name}";
    }
}
