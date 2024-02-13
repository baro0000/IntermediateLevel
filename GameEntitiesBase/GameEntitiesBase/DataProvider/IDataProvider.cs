using GameEntitiesBase.Entities;

namespace GameEntitiesBase.DataProvider
{
    public interface IDataProvider
    {
        //GameMaster CreateNewGameMaster(int id);
        Npc CreateNewNpc(int id, int level = 1);
        Player CreateNewPlayer(int id);
        //List<GameMaster> ProvideGMs();
        List<Npc> ProvideNpcs();
        List<Player> ProvidePlayers();
    }
}