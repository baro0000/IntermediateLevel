using GameEntitiesBase.Data.Entities;

namespace GameEntitiesBase._2_ApplicationServices
{
    public interface IFightMenager
    {
        void Run_PlayerVsNpc(Player player, Npc npc);
    }
}