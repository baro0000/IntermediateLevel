using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Data.Entities
{
    public interface IEntity
    {
        int Id { get; }
        Gender Sex { get; }
        string Name { get; }
        int Level { get; set; }
        Race Race { get; }
        Profession Profession { get; }


        int Atack();
        void TakeDamage(int damageValue);
    }
}
