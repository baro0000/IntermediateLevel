using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Data.Entities
{
    public abstract class EntityBase : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Gender Sex { get; set; }
        public string Name { get; set; }
        public Race Race { get; set; }
        public Profession Profession { get; set; }
        public int StatisticsId { get; set; }
        public Statistics Stats { get; set; }
        public int Level { get; set; }
        private int initialDamage = 6;

        public EntityBase()
        {
        }
        public EntityBase(Gender sex, string name, Race race, Profession profession, Statistics stats, int level)
        {
            Sex = sex;
            Name = name;
            Race = race;
            Profession = profession;
            Stats = stats;
            Level = level;
        }

        public virtual int Atack()
        {
            var professionBonus = CalculateProfessionBonusForAttack();

            return initialDamage + professionBonus;
        }
        public virtual void TakeDamage(int damageValue)
        {
            Stats.CurrentHitPoints -= damageValue;
        }

        public abstract TargetArea ChooseArea();

        public virtual void HealAllWounds()
        {
            Stats.CurrentHitPoints = Stats.TotalHitPoints;
        }

        protected virtual int CalculateProfessionBonusForAttack()
        {
            switch (Profession)
            {
                case Profession.Knight:
                    return 3;
                case Profession.Barbarian:
                    return 4;
                default:
                    return 3;
            }
        }
    }
}
