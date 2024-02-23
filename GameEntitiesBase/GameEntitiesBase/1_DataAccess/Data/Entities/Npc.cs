using GameEntitiesBase.Data.Entities.Extensions;
using System.Text;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Data.Entities
{
    public class Npc : EntityBase, IEntityWithStatistics
    {
        public Npc()
        {

        }
        public Npc(Gender sex, string name, Race race, Profession profession, Statistics stats, int level) : base(sex, name, race, profession, stats, level)
        {
        }

        public override TargetArea ChooseArea()
        {
            Random random = new Random();
            var strikeOption = random.Next(0, 3);

            switch (strikeOption)
            {
                case 0:
                    return TargetArea.High;
                case 1:
                    return TargetArea.Middle;
                case 2:
                    return TargetArea.Low;
                default:
                    throw new Exception("Couldn't return value");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($">> NPC Id: {Id}  <<");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Race: {Race}");
            sb.AppendLine($"Profession: {Profession}");
            sb.AppendLine($"Stats:");
            sb.AppendLine($"Strength: {Stats.Strength}");
            sb.AppendLine($"Agility: {Stats.Agility}");
            sb.AppendLine($"HP: {Stats.CurrentHitPoints} / {Stats.TotalHitPoints}");

            return sb.ToString();
        }
    }
}
