using GameEntitiesBase.Data.Entities;
using System.Threading;

namespace GameEntitiesBase._2_ApplicationServices
{
    public class FightMenager
    {
        private Random _random;

        public FightMenager()
        {
            _random = new Random();
        }

        public void Run_PlayerVsNpc(Player player, Npc npc)
        {
            DrawStartingSidePlayerVsNpc(player, npc);


        }

        private void DrawStartingSidePlayerVsNpc(Player player, Npc npc)
        {
            Console.WriteLine(">>> Welcome to Arena <<<");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Drawing the starting player");
            Thread.Sleep(1000);
            Console.WriteLine("Press any key to toss k20");
            Console.ReadLine();
            var playerScore = _random.Next(1, 20);
            Console.WriteLine($"Your score is: {playerScore}");
            Console.WriteLine();
            Console.WriteLine($"Opponent {npc.Name} tossing dice...");
            var npcScore = _random.Next(1, 20);
            Thread.Sleep(1000);
            Console.WriteLine($"Opponent score: {npcScore}");
            var whoStart = (playerScore > npcScore) ? $"{player.Name} starts battle" : $"{npc.Name} starts battle";
            Thread.Sleep(1000);
            Console.WriteLine(whoStart);
            Thread.Sleep(1000);
            Console.WriteLine("Press any key to begin...");
            Console.ReadLine();
            for (int i = 5; i > 0; i--)
            {
                Console.Clear();
                Console.Write("BATTLE BEGINS IN: ");
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}
