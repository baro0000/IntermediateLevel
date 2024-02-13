using static GameEntitiesBase.Entities.Available;

namespace GameEntitiesBase.Entities
{
    public interface IEntity
    {
        int Id { get; }
        Gender Sex { get; }
        string Name { get; }
        int Level { get; set; }
        Race Race { get; }
        Profession Profession { get; }
        Statistics Stats { get; }


        int Atack();
        int Defend();
        void TakeDamage(int damageValue);
    }
}
