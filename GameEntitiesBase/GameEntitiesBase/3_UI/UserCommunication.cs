using GameEntitiesBase._2_ApplicationServices;
using GameEntitiesBase.Components.DataProvider;
using GameEntitiesBase.Data.Entities;
using GameEntitiesBase.Data.Repositories;
using GameEntitiesBase.Data.Repositories.Extensions;

namespace GameEntitiesBase
{
    public class UserCommunication : IUserCommunication
    {
        private IRepository<Player> _repositoryPlayers;
        private IRepository<Npc> _repositoryNpcs;

        private List<Player> newAddedPlayers = new List<Player>();
        private List<Npc> newAddedNpcs = new List<Npc>();

        private int currentOption;
        private int exitMainMenu = 0;
        private string[] mainMenuOptions = { "Show list of entities",
                                                                  "Add new entity",
                                                                  "Delete entity",
                                                                  "Find entity",
                                                                  "Battle",
                                                                  "Quit" };
        private delegate void PrintChosenMenu();
        private IDataProvider _dataProvider;
        private IFightMenager _fightMenager;
        bool _offlineMode;

        public UserCommunication(IRepository<Player> repositoryPlayer, IRepository<Npc> repositoryNPC, IDataProvider dataProvider, IFightMenager fightMenager)
        {
            currentOption = 0;
            _dataProvider = dataProvider;
            _repositoryPlayers = repositoryPlayer;
            _repositoryNpcs = repositoryNPC;
            _fightMenager = fightMenager;
        }

        private void PrintMainMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Welcome to the game entities database <<<");
            if (_offlineMode)
            {
                Console.WriteLine("---! OFFLINE MODE !---");
            }
            Console.WriteLine();

            OptionsDisplay(mainMenuOptions, currentOption);
        }

        private void OptionsDisplay(string[] givenMenuOptions, int indicator)
        {
            for (int i = 0; i < givenMenuOptions.Length; i++)
            {
                if (indicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{givenMenuOptions[i],-35}");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(givenMenuOptions[i]);
                }
            }
        }
        public void MainMenu(bool isOfflineActivated)
        {
            _offlineMode = isOfflineActivated;
            Console.Title = "Game Entities Base";
            Console.CursorVisible = false;

            while (true)
            {
                if (exitMainMenu == 1)
                {
                    break;
                }
                Console.Clear();

                PrintMainMenu();
                ChooseOption(mainMenuOptions, ref currentOption, PrintMainMenu);
                RunChosenOption();
            }
        }

        private void RunChosenOption()
        {
            switch (currentOption)
            {
                case 0:
                    DisplayEntitiesMenu();
                    break;
                case 1:
                    if (_offlineMode)
                    {
                        Console.WriteLine("Adding new entity is not possible in offline mode");
                        Console.WriteLine("Press any key to return");
                        Console.ReadKey();
                    }
                    else
                    {
                        AddingNewEntityLoop();
                    }
                    break;
                case 2:
                    if (_offlineMode)
                    {
                        Console.WriteLine("Deleting entity is not possible in offline mode");
                        Console.WriteLine("Press any key to return");
                        Console.ReadKey();
                    }
                    else
                    {
                        DeleteItem();
                    }
                    break;
                case 3:
                    FindAndDisplay();
                    break;
                case 4:
                    RunBattle();
                    break;
                case 5:
                    _repositoryPlayers.AddBatch(newAddedPlayers);
                    _repositoryNpcs.AddBatch(newAddedNpcs);
                    exitMainMenu = 1;
                    break;
            }
        }

        private void RunBattle()
        {
            Console.Clear();

            var listPlayers = GetPlayersFromBases();
            var listNpc = GetNpcsFromBases();
            Player player = default;
            Npc npc = default;

            do
            {
                Console.WriteLine("Input Id of player:");
                var input = Console.ReadLine();
                int.TryParse(input, out int id);
                player = listPlayers.SingleOrDefault(x => x.Id == id);
                if (player == null)
                {
                    Console.WriteLine("Player not found!");
                }
            } while (player == null);

            Console.Clear();
            Console.WriteLine(player);
            Console.WriteLine();

            do
            {
                Console.WriteLine("Input Id of npc:");
                var input = Console.ReadLine();
                int.TryParse(input, out int id);
                npc = listNpc.SingleOrDefault(x => x.Id == id);
                if (npc == null)
                {
                    Console.WriteLine("Npc not found!");
                }
            } while (player == null);

            Console.Clear();
            Console.WriteLine(player);
            Console.WriteLine();
            Console.WriteLine(npc);
            Console.WriteLine();

            Console.WriteLine("Pres any key to continue");
            Console.ReadKey();

            _fightMenager.Run_PlayerVsNpc(player, npc);
        }

        private void DisplayEntitiesMenu()
        {
            int chosenOption = 0;
            string[] sortingMenuOptions = { "Show all", "Sort by strength", "Sort by agility", "Return to main menu" };
            void PrintSortingMenu()
            {
                Console.Clear();
                Console.WriteLine(">>> Display Menu <<<");
                OptionsDisplay(sortingMenuOptions, chosenOption);
            }

            PrintSortingMenu();
            ChooseOption(sortingMenuOptions, ref chosenOption, PrintSortingMenu);

            void DisplayPlayerAndNpcLists(List<Player> players, List<Npc> npcs)
            {
                Console.WriteLine(">>> PLAYERS <<<");
                Console.WriteLine();
                foreach (var item in players)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine(">>> NPCS <<<");
                Console.WriteLine();
                foreach (var item in npcs)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to return");
                Console.ReadLine();
            }

            switch (chosenOption)
            {
                case 0:
                    Console.WriteLine("All entities: ");
                    Console.WriteLine();
                    var playerList = GetPlayersFromBases().OrderBy(obj => obj.Id).ToList();
                    var npcList = GetNpcsFromBases().OrderBy(obj => obj.Id).ToList();
                    DisplayPlayerAndNpcLists(playerList, npcList);
                    break;
                case 1:
                    Console.WriteLine("All entities: ");
                    Console.WriteLine();
                    playerList = GetPlayersFromBases().OrderBy(obj => obj.Stats.Strength).ToList();
                    npcList = GetNpcsFromBases().OrderBy(obj => obj.Stats.Strength).ToList();
                    DisplayPlayerAndNpcLists(playerList, npcList);
                    break;
                case 2:
                    Console.WriteLine("All entities: ");
                    Console.WriteLine();
                    playerList = GetPlayersFromBases().OrderBy(obj => obj.Stats.Agility).ToList();
                    npcList = GetNpcsFromBases().OrderBy(obj => obj.Stats.Agility).ToList();
                    DisplayPlayerAndNpcLists(playerList, npcList);
                    break;
                case 3:
                    break;
            }
        }

        private void AddingNewEntityLoop()
        {
            Console.Clear();
            while (true)
            {
                AddNewEntity();
                Console.Write("Do you want to add next entity? If you want to quit press N or Esc, or any key to continue");
                ConsoleKeyInfo chosenKey = Console.ReadKey();
                Console.WriteLine();

                if (chosenKey.Key == ConsoleKey.Y)
                {
                    continue;
                }
                else if (chosenKey.Key == ConsoleKey.N || chosenKey.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private List<Player> GetPlayersFromBases()
        {
            List<Player> list = new List<Player>();
            list.AddRange(_repositoryPlayers.GetAll());
            list.AddRange(newAddedPlayers);

            return list;
        }

        private List<Npc> GetNpcsFromBases()
        {
            List<Npc> list = new List<Npc>();
            list.AddRange(_repositoryNpcs.GetAll());
            list.AddRange(newAddedNpcs);

            return list;
        }

        private void DeleteItem()
        {
            int chosenOption = 0;
            string[] deleteMenuOptions = { "Delete player", "Delete npc", "Return to main menu" };
            void PrintDeleteMenu()
            {
                Console.Clear();
                Console.WriteLine(">>> Delete Entity <<<");
                OptionsDisplay(deleteMenuOptions, chosenOption);
            }

            PrintDeleteMenu();
            ChooseOption(deleteMenuOptions, ref chosenOption, PrintDeleteMenu);

            Console.Clear();
            Console.WriteLine("Input Id of entity you want to delete");
            var input = Console.ReadLine();
            int.TryParse(input, out int id);

            switch (chosenOption)
            {
                case 0:
                    var players = GetPlayersFromBases();
                    var player = players.SingleOrDefault(x => x.Id == id);
                    if (player != null)
                    {
                        _repositoryPlayers.Remove(player);
                        _repositoryPlayers.Save();
                    }
                    else
                    {
                        Console.WriteLine("Player not found");
                    }
                    break;
                case 1:
                    var npcs = GetNpcsFromBases();
                    var npc = npcs.SingleOrDefault(x => x.Id == id);
                    if (npc != null)
                    {
                        _repositoryNpcs.Remove(npc);
                        _repositoryNpcs.Save();
                    }
                    else
                    {
                        Console.WriteLine("Npc not found");
                    }
                    break;
                case 2:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to return...");
            Console.ReadLine();
        }
        private void FindAndDisplay()
        {
            int chosenOption = 0;
            string[] findMenuOptions = { "Find player", "Find npc", "Return to main menu" };
            void PrintFindMenu()
            {
                Console.Clear();
                Console.WriteLine(">>> Find Entity <<<");
                OptionsDisplay(findMenuOptions, chosenOption);
            }

            PrintFindMenu();
            ChooseOption(findMenuOptions, ref chosenOption, PrintFindMenu);

            int TakeIdFromUser()
            {
                Console.Clear();
                Console.WriteLine("Input Id of entity you want to display");
                var input = Console.ReadLine();
                int.TryParse(input, out int id);
                return id;
            }

            switch (chosenOption)
            {
                case 0:
                    var players = GetPlayersFromBases();
                    var id = TakeIdFromUser();
                    var player = players.SingleOrDefault(x => x.Id == id);
                    Console.WriteLine((player != null) ? player.ToString() : "Player not found");
                    break;
                case 1:
                    var npcs = GetNpcsFromBases();
                    id = TakeIdFromUser();
                    var npc = npcs.SingleOrDefault(x => x.Id == id);

                    Console.WriteLine((npc != null) ? npc.ToString() : "Player not found");
                    break;
                case 2:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to return...");
            Console.ReadLine();
        }

        private void ChooseOption(string[] givenMenuOptions, ref int indicator, PrintChosenMenu printChosenMenu)
        {
            do
            {
                ConsoleKeyInfo chosenKey = Console.ReadKey();

                if (chosenKey.Key == ConsoleKey.UpArrow)
                {
                    indicator = (indicator > 0) ? indicator - 1 : givenMenuOptions.Length - 1;
                    printChosenMenu();
                }
                else if (chosenKey.Key == ConsoleKey.DownArrow)
                {
                    indicator = (indicator < givenMenuOptions.Length - 1) ? indicator + 1 : indicator = 0;
                    printChosenMenu();
                }
                else if (chosenKey.Key == ConsoleKey.Escape)
                {
                    indicator = givenMenuOptions.Length - 1;
                    break;
                }
                else if (chosenKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }

        private int AddNewEntity()
        {
            int activeOption = 0;
            string[] entityCreatingOptions = { "Player", "Npc", "Return" };

            PrintCreatingNewEntitiyMenu();
            ChooseOption(entityCreatingOptions, ref activeOption, PrintCreatingNewEntitiyMenu);
            switch (activeOption)
            {
                case 0:
                    Console.Clear();
                    Player player = _dataProvider.CreateNewPlayer();
                    newAddedPlayers.Add(player);
                    return 0;
                case 1:
                    Console.Clear();
                    Npc npc = _dataProvider.CreateNewNpc();
                    newAddedNpcs.Add(npc);
                    return 0;
                case 2:
                    return 0;
                default:
                    return 0;
            }

            void PrintCreatingNewEntitiyMenu()
            {
                Console.Clear();
                Console.WriteLine(">>> New entity creator <<<");
                OptionsDisplay(entityCreatingOptions, activeOption);
            }
        }
    }
}
