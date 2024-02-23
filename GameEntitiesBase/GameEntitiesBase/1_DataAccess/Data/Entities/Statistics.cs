using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Data.Entities
{
    public class Statistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TotalHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }

        public Statistics()
        {

        }
        public Statistics(int strength, int agility, Race race)
        {
            Strength = strength;
            Agility = agility;
            CalculateHP(race);
            CurrentHitPoints = TotalHitPoints;
        }

        private void CalculateHP(Race race)
        {
            int basicHP = 50;

            switch (race)
            {
                case Race.Dwarf:
                    basicHP += 20;
                    Strength += 2;
                    Agility -= 2;
                    break;
                case Race.Human:
                    basicHP += 15;
                    Strength += 1;
                    break;
                case Race.Elf:
                    basicHP += 10;
                    Agility += 2;
                    break;
            }

            TotalHitPoints = basicHP;
        }

        public int CalculateStrengthBonus()
        {
            return (Strength > 10) ? (Strength - 10) / 2 : 0;
        }

        public int CalculateAgilityBonus()
        {
            return (Agility > 0) ? (Agility - 10) / 2 : 0;
        }
    }
}
