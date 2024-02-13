using GameEntitiesBase.Data;
using GameEntitiesBase.DataProvider;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;
using GameEntitiesBase.Repositories.Extensions;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace GameEntitiesBase
{
    public class MenuGenerator : IUserCommunication
    {

        // Repozytoria baz danych
        private IRepository<Player> _repositoryPlayer;
        private IRepository<Npc> _repositoryNPC;

        //Tymczasowe listy do dodawania obiektów grupami
        private List<Player> newAddedPlayers = new List<Player>();
        private List<Npc> newAddedNpcs = new List<Npc>();

        private int currentOption;
        private int exitMainMenu = 0;
        private string[] mainMenuOptions = { "Show list of entities",
                                                                  "Add new entity",
                                                                  "Delete entity",
                                                                  "Find entity",
                                                                  "Quit" };
        private delegate void PrintChosenMenu();
        private IDataProvider _dataProvider;

        public MenuGenerator(IRepository<Player> repositoryPlayer, IRepository<Npc> repositoryNPC, IDataProvider dataProvider)
        {
            currentOption = 0;
            _dataProvider = dataProvider;
            _repositoryPlayer = repositoryPlayer;
            _repositoryNPC = repositoryNPC;
        }

        private void PrintMainMenu() //wyświetla menu i podświetla aktywną opcję
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Welcome to the game entities database <<<");
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
        public void MainMenu()
        {
            Console.Title = "Game Entities Base";
            Console.CursorVisible = false;

            while (true)
            {
                if (exitMainMenu == 1)
                {
                    break;
                }
                PrintMainMenu();
                ChooseOption(mainMenuOptions, ref currentOption, PrintMainMenu);
                RunChosenOption();
            }
        }

        private void RunChosenOption() // Główna logika wybieranych opcji
        {
            switch (currentOption)
            {
                case 0:   //Show list of entities
                    Console.Clear();

                    Console.WriteLine("All entities: ");
                    Console.WriteLine();
                    var sortedList = GetEntitiesFromAllBases();

                    foreach (var item in sortedList)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    Console.Clear();
                    //ClearConsoleBuffer();
                    break;
                case 1:   //Add new entity
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

                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
                case 2:    //Delete entity
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to delete");
                    var input = Console.ReadLine();
                    int.TryParse(input, out int result);

                    DeleteItem(result);

                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
                case 3:   //Find entity
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to display");
                    var input2 = Console.ReadLine();
                    int.TryParse(input2, out int result2);

                    FindAndDisplay(result2);

                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
                case 4:   //Exit
                    _repositoryPlayer.AddBatch(newAddedPlayers);
                    _repositoryNPC.AddBatch(newAddedNpcs);
                    exitMainMenu = 1;
                    break;
            }
        }

        //private void ClearConsoleBuffer()
        //{
        //    int linesToClear = 20;

        //    for (int i = 0; i < linesToClear; i++)
        //    {
        //        Console.WriteLine();
        //    }
        //}

        private List<EntityBase> GetEntitiesFromAllBases()
        {
            List<EntityBase> list = new List<EntityBase>();
            list.AddRange(_repositoryPlayer.GetAll());
            list.AddRange(_repositoryNPC.GetAll());
            list.AddRange(newAddedNpcs);
            list.AddRange(newAddedPlayers);

            return list.OrderBy(obj => obj.Id).ToList();
        }

        private void DeleteItem(int id)
        {
            var entity1 = _repositoryPlayer.GetById(id);
            var entity2 = _repositoryNPC.GetById(id);
            var entity3 = newAddedPlayers.SingleOrDefault(obj => obj.Id == id);
            var entity4 = newAddedNpcs.SingleOrDefault(obj => obj.Id == id);

            if (entity1 != null)
            {
                _repositoryPlayer.Remove(entity1);
                _repositoryPlayer.Save();
            }
            else if (entity2 != null)
            {
                _repositoryNPC.Remove(entity2);
                _repositoryNPC.Save();
            }
            else if (entity3 != null)
            {
                newAddedPlayers.Remove(entity3);
                Console.WriteLine($"{entity3.Name} removed from database");
            }
            else if (entity4 != null)
            {
                newAddedNpcs.Remove(entity4);
                Console.WriteLine($"{entity4.Name} removed from database");
            }
            else
            {
                Console.WriteLine("Entity not found");
            }
        }
        private void FindAndDisplay(int id)
        {
            var entity1 = _repositoryPlayer.GetById(id);
            var entity2 = _repositoryNPC.GetById(id);
            var entity3 = newAddedPlayers.SingleOrDefault(obj => obj.Id == id);
            var entity4 = newAddedNpcs.SingleOrDefault(obj => obj.Id == id);

            if (entity1 != null)
            {
                Console.WriteLine(entity1);
            }
            else if (entity2 != null)
            {
                Console.WriteLine(entity2);
            }
            else if (entity3 != null)
            {
                Console.WriteLine(entity3);
            }
            else if (entity4 != null)
            {
                Console.WriteLine(entity4);
            }
            else
            {
                Console.WriteLine("Entity not found");
            }
        }

        private void ChooseOption(string[] givenMenuOptions, ref int indicator, PrintChosenMenu printChosenMenu) //Obsługa ruchu strzałek i przycisków
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

        private void AddNewEntity()
        {
            int activeOption = 0;
            string[] entityCreatingOptions = ["Player", "Npc", "Return"];

            PrintCreatingNewEntitiyMenu();
            ChooseOption(entityCreatingOptions, ref activeOption, PrintCreatingNewEntitiyMenu);
            switch (activeOption)
            {
                case 0: // Player
                    Console.Clear();
                    Player player = _dataProvider.CreateNewPlayer(CheckAvailability());
                    newAddedPlayers.Add(player);
                    break;
                case 1: // Npc
                    Console.Clear();
                    Npc npc = _dataProvider.CreateNewNpc(CheckAvailability());
                    newAddedNpcs.Add(npc);
                    break;
                case 2: //Exit
                    MainMenu();
                    break;
            }

            void PrintCreatingNewEntitiyMenu()
            {
                Console.Clear();
                Console.WriteLine(">>> New entity creator <<<");
                OptionsDisplay(entityCreatingOptions, activeOption);
            }

            int CheckAvailability()
            {
                List<int> unavailableIds = new List<int>();
                int result = 1;

                foreach (var obj in newAddedPlayers)
                {
                    unavailableIds.Add(obj.Id);
                }
                foreach (var obj in newAddedNpcs)
                {
                    unavailableIds.Add(obj.Id);
                }
                foreach (var entity in _repositoryPlayer.GetAll())
                {
                    unavailableIds.Add(entity.Id);
                }
                foreach (var entity in _repositoryNPC.GetAll())
                {
                    unavailableIds.Add(entity.Id);
                }

                while (true)
                {
                    if (!unavailableIds.Contains(result))
                    {
                        break;
                    }
                    result++;
                }

                return result;
            }
        }
    }
}
