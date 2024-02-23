using GameEntitiesBase.Data.Entities.Extensions;
using System.Text;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Data.Entities
{
    public class Player : EntityBase, IEntityWithStatistics
    {
        public Player()
        {

        }
        public Player(Gender sex, string name, Race race, Profession profession, Statistics stats, int level) : base(sex, name, race, profession, stats, level)
        {
        }

        public override TargetArea ChooseArea()
        {
            int userInput;
            while (true)
            {
                Console.WriteLine("Choose area:");
                Console.WriteLine("1. High");
                Console.WriteLine("2. Middle");
                Console.WriteLine("3. Low");

                while (true)
                {
                    Console.Write("Your choice: ");
                    bool isInputCorrect = int.TryParse(Console.ReadLine(), out userInput);
                    Console.WriteLine();
                    userInput--;
                    if (isInputCorrect && userInput >= 0 && userInput <= 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input. Please enter correct number");
                    }
                }

                switch (userInput)
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
