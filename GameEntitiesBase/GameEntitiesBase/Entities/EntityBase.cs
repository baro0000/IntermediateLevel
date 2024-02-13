using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static GameEntitiesBase.Entities.Available;

namespace GameEntitiesBase.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; }
        public Gender Sex { get; }
        public string Name { get; }
        public Race Race { get; }
        public Profession Profession { get;  }
        public Statistics Stats {  get; }
        public int Level { get; set; }
        private int initialDamage = 8;
        private int initialDefence = 10;
        
        public EntityBase(int id, Gender sex, string name, Race race, Profession profession, Statistics stats, int level)
        {
            Id = id;
            Sex = sex;
            Name = name;
            Race = race;
            Profession = profession;
            Stats = stats;
            Level = level;
        }

        public virtual int Atack()
        {
            var professionBonus = CalculateProfessionBonus();

            return initialDamage + professionBonus;
        }
        public virtual int Defend()
        {
            var professionBonus = CalculateProfessionBonus();

            return initialDefence + professionBonus;
        }
        public virtual void TakeDamage(int damageValue)
        {
            Stats.CurrentHitPoints -= damageValue;
        }
        
        protected virtual int CalculateProfessionBonus()
        {
            switch (Profession)
            {
                case Available.Profession.Knight:
                    return 4;
                case Available.Profession.Barbarian:
                    return 3;
                default:
                    return 3;
            }
        }
    }
}
