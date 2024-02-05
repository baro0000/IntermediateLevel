namespace GameEntitiesBase.Entities
{
    public class Npc : Player
    {
        public Npc()
        {
            
        }

        public Npc(string name) : base(name) 
        {
            
        }

        public override string ToString() => $"{base.ToString()} (NPC)";
    }
}
