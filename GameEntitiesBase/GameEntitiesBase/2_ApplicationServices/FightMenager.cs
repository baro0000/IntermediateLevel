using GameEntitiesBase.Data.Entities;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase._2_ApplicationServices
{
    public class FightMenager : IFightMenager
    {
        private Random _random;

        public FightMenager()
        {
            _random = new Random();
        }

        public void Run_PlayerVsNpc(Player player, Npc npc)
        {
            SetConsoleToBlack();
            Console.Clear();

            var startingSide = DrawStartingSide_PlayerVsNpc(player, npc);
            try
            {
                RunBattleMechanism(player, npc, startingSide);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            player.HealAllWounds();
            npc.HealAllWounds();
        }

        private void SetConsoleToBlack()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void RunBattleMechanism(Player player, Npc npc, int startingSide)
        {
            var roundChanger = startingSide;
            int playerScoreWithBonus = 0,
                npcScoreWithBonus = 0,
                playerScore = 0,
                npcScore = 0,
                playerStrengthBonus = player.Stats.CalculateStrengthBonus(),
                npcStrengthBonus = npc.Stats.CalculateStrengthBonus(),
                playerAgilityBonus = player.Stats.CalculateAgilityBonus(),
                npcAgilityBonus = npc.Stats.CalculateAgilityBonus();

            Console.Clear();

            while (player.Stats.CurrentHitPoints > 0 && npc.Stats.CurrentHitPoints > 0)
            {
                Console.Clear();
                CommunicatesMenager.GreenText($"{player.Name} hp: {player.Stats.CurrentHitPoints}");
                CommunicatesMenager.RedText($"{npc.Name} hp: {npc.Stats.CurrentHitPoints}");
                Console.WriteLine();

                switch (roundChanger)
                {
                    case 0:
                        NpcTurn(player, npc, ref playerScoreWithBonus, ref npcScoreWithBonus, ref playerScore, ref npcScore, npcStrengthBonus, playerAgilityBonus);

                        roundChanger = 1;
                        Console.WriteLine();
                        Console.WriteLine("Press any key for next round");
                        Console.ReadKey();
                        break;
                    case 1:
                        PlayerTurn(player, npc, ref playerScoreWithBonus, ref npcScoreWithBonus, ref playerScore, ref npcScore, playerStrengthBonus, npcAgilityBonus);

                        roundChanger = 0;
                        Console.WriteLine();
                        Console.WriteLine("Press any key for next round");
                        Console.ReadKey();
                        break;
                }
            }
            Console.Clear();
            if (player.Stats.CurrentHitPoints > 0 && npc.Stats.CurrentHitPoints <= 0)
            {
                Console.WriteLine(CommunicatesMenager.PerformFinisherByPlayer());
                Console.WriteLine();
                CommunicatesMenager.GreenText($"Player {player.Name} wins battle!");
            }
            else if ((player.Stats.CurrentHitPoints <= 0 && npc.Stats.CurrentHitPoints > 0))
            {
                Console.WriteLine(CommunicatesMenager.PerformFinisherByNpc());
                Console.WriteLine();
                CommunicatesMenager.RedText($"Opponent {npc.Name} wins battle!");
            }
            else
            {
                throw new Exception("Unsupported fight result");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        private void PlayerTurn(Player player, Npc npc, ref int playerScoreWithBonus, ref int npcScoreWithBonus, ref int playerScore, ref int npcScore, int playerStrengthBonus, int npcAgilityBonus)
        {
            CommunicatesMenager.BlueText(">>>> PLAYER ROUND <<<<");
            Console.WriteLine("It is your time to strike!");
            Console.WriteLine("Set area to set up your attack");
            var playerAttack = player.ChooseArea();
            var npcDefence = npc.ChooseArea();

            if (playerAttack == npcDefence)
            {
                Console.WriteLine();
                CommunicatesMenager.YellowText(CommunicatesMenager.AttackParriedByNpc());
            }
            else
            {
                Console.WriteLine($"Opponent Tried defend {npcDefence} area, it is your chance to strike! Press any key to toss k20");
                Console.ReadKey();

                playerScore = _random.Next(1, 21);
                playerScoreWithBonus = playerScore + playerStrengthBonus;
                Console.WriteLine($"Your score is: {playerScoreWithBonus}");

                npcScore = _random.Next(1, 21);
                npcScoreWithBonus = npcScore + npcAgilityBonus;
                Console.WriteLine($"Opponent score is: {npcScoreWithBonus}");
                Console.WriteLine();

                if (playerScoreWithBonus < npcScoreWithBonus)
                {
                    CommunicatesMenager.BlueText(CommunicatesMenager.AttackDodgedByNpc());
                }
                else
                {
                    var damage = _random.Next(1, player.Atack());
                    damage += playerStrengthBonus;

                    bool isCriticalStrikeOrCriticalDefenceFail = playerScore == 20 || npcScore == 1;
                    if (isCriticalStrikeOrCriticalDefenceFail)
                    {
                        CommunicatesMenager.DarkYellowText("! CRITICAL !");
                        damage = damage * 2;
                    }

                    CommunicatesMenager.RedText(CommunicatesMenager.NpcTakeDamage() + $" Damage value: {damage}.");
                    npc.TakeDamage(damage);
                    CommunicatesMenager.GreenText($"Remaining health: {npc.Stats.CurrentHitPoints}");
                }
            }
        }

        private void NpcTurn(Player player, Npc npc, ref int playerScoreWithBonus, ref int npcScoreWithBonus, ref int playerScore, ref int npcScore, int npcStrengthBonus, int playerAgilityBonus)
        {
            CommunicatesMenager.BlueText(">>>> NPC ROUND <<<<");
            Console.WriteLine("Opponent is about to attack!");
            Console.WriteLine("Choose area to set up your guard");
            TargetArea playerDefence = player.ChooseArea();
            TargetArea npcAttack = npc.ChooseArea();

            if (playerDefence == npcAttack)
            {
                Console.WriteLine();
                CommunicatesMenager.YellowText(CommunicatesMenager.AttackParriedByPlayer());
            }
            else
            {
                Console.WriteLine($"Opponent performed {npcAttack} attack! Press any key to toss k20");
                Console.ReadKey();

                playerScore = _random.Next(1, 21);
                playerScoreWithBonus = playerScore + playerAgilityBonus;
                Console.WriteLine($"Your score is: {playerScoreWithBonus}");

                npcScore = _random.Next(1, 21);
                npcScoreWithBonus = npcScore + npcStrengthBonus;
                Console.WriteLine($"Opponent score is: {npcScoreWithBonus}");
                Console.WriteLine();

                if (playerScoreWithBonus > npcScoreWithBonus)
                {
                    CommunicatesMenager.BlueText(CommunicatesMenager.AttackDodgedByPlayer());
                }
                else
                {
                    var damage = _random.Next((1), (npc.Atack()));
                    damage += npcStrengthBonus;

                    bool isCriticalStrikeOrCriticalDefenceFail = playerScore == 1 || npcScore == 20;
                    if (isCriticalStrikeOrCriticalDefenceFail)
                    {
                        CommunicatesMenager.DarkYellowText("! CRITICAL !");
                        damage = damage * 2;
                    }

                    CommunicatesMenager.RedText(CommunicatesMenager.PlayerTakeDamage() + $" Damage value: {damage}.");
                    player.TakeDamage(damage);
                    CommunicatesMenager.GreenText($"Remaining health: {player.Stats.CurrentHitPoints}");
                }
            }
        }

        private int DrawStartingSide_PlayerVsNpc(Player player, Npc npc)
        {
            Console.WriteLine(">>> Welcome to Arena <<<");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Drawing the starting player");
            Thread.Sleep(1000);

            string whoStartComunicate;
            int whoStartInt;
            bool isWinnerNotChosen;
            do
            {
                isWinnerNotChosen = RollDiceUntilWinner(player, npc, out whoStartComunicate, out whoStartInt);
            } while (isWinnerNotChosen);
            Thread.Sleep(1000);
            Console.WriteLine(whoStartComunicate);
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
            return whoStartInt;
        }

        private bool RollDiceUntilWinner(Player player, Npc npc, out string whoStartComunicate, out int whoStartInt)
        {
            Console.WriteLine("Press any key to toss k20");
            Console.ReadLine();
            var playerScore = _random.Next(1, 20);
            Console.WriteLine($"Your score is: {playerScore}");
            Console.WriteLine();

            Console.WriteLine($"Opponent {npc.Name} tossing dice...");
            var npcScore = _random.Next(1, 20);
            Thread.Sleep(1000);
            Console.WriteLine($"Opponent score: {npcScore}");
            Console.WriteLine();

            whoStartComunicate = "";
            whoStartInt = 0;
            if (playerScore > npcScore)
            {
                whoStartComunicate = $"{player.Name} starts battle";
                whoStartInt = 1;
                return false;
            }
            else if (playerScore < npcScore)
            {
                whoStartComunicate = $"{npc.Name} starts battle";
                whoStartInt = 0;
                return false;
            }
            else
            {
                Console.WriteLine("Scores are equal! Press any key to toss again");
                return true;
            }
        }
    }
}
