using GameEntitiesBase.Data.Entities;

namespace GameEntitiesBase.Components.DataProvider
{
    public interface IDataProvider
    {
        Npc CreateNewNpc(int level = 1);
        Player CreateNewPlayer();
        List<Npc> ProvideNpcs();
        List<Player> ProvidePlayers();
    }
}