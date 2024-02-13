using System.Text;
using static GameEntitiesBase.Entities.Available;

namespace GameEntitiesBase.Entities
{
    public class Player : EntityBase
    {
        
        public Player(int id, Gender sex, string name, Race race, Profession profession, Statistics stats, int level) : base(id, sex, name, race, profession, stats, level)
        {
        }
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($">> PLAYER Id: {Id}  <<");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Race: {Race}");
            sb.AppendLine($" Profession: {Profession}");
            sb.AppendLine($"Stats:");
            sb.AppendLine($"Strength: {Stats.Strength}");
            sb.AppendLine($"Agility: {Stats.Agility}");
            sb.AppendLine($"HP: {Stats.CurrentHitPoints} / {Stats.TotalHitPoints}");

            return sb.ToString();
        }
    }
}
