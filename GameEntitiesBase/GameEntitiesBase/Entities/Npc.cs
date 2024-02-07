namespace GameEntitiesBase.Entities
{
    public class Npc : EntityBase
    {
        public Npc()
        {
            
        }

        public Npc(string name)
        {
            Name = name;
        }

        public override string ToString() => $"Id: {Id} Name: {Name} (NPC)";
    }
}
