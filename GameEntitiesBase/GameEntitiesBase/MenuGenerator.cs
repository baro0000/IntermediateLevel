using GameEntitiesBase.Data;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;
using GameEntitiesBase.Repositories.Extensions;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace GameEntitiesBase
{
    public class MenuGenerator
    {
        private string filePath = "savePlayerAndNpc.txt";
        private string filePath2 = "saveGM.txt";
        private string filePath3 = "saveNPC.txt";

        private SqlEntityRepository<Player> repositoryPlayer = new SqlEntityRepository<Player>(new GameEntitiesBaseDbContext());
        private SqlEntityRepository<GameMaster> repositoryGM = new SqlEntityRepository<GameMaster>(new GameEntitiesBaseDbContext());
        private SqlEntityRepository<Npc> repositoryNPC = new SqlEntityRepository<Npc>(new GameEntitiesBaseDbContext());

        private List<Player> newAddedPlayers = new List<Player>();
        private List<Npc> newAddedNpcs = new List<Npc>();
        private List<GameMaster> newAddedGMs = new List<GameMaster>();

        private int currentOption;
        private string[] menuOptions = { "Show list of entities",
                                                                  "Add new entity",
                                                                  "Delete entity",
                                                                  "Find entity",
                                                                  "Quit" };
        private delegate void PrintChosenMenu();

        public MenuGenerator()
        {
            currentOption = 0;
            repositoryPlayer.LoadFromFile(filePath);
            repositoryGM.LoadFromFile(filePath2);
            repositoryNPC.LoadFromFile(filePath3);
            repositoryPlayer.ItemAdded += ObjectAdded;
            repositoryPlayer.ItemRemoved += ObjectRemoved;
        }

        void ObjectAdded(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} added to database");
        }
        void ObjectRemoved(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} removed from database");
        }

        private void PrintMainMenu() //wyświetla menu i podświetla aktywną opcję
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Welcome to the game entities database <<<");
            Console.WriteLine();

            OptionsDisplay(menuOptions, currentOption);
        }

        public void OptionsDisplay(string[] givenMenuOptions, int indicator)
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
                PrintMainMenu();
                ChooseOption(menuOptions, ref currentOption, PrintMainMenu);
                RunChosenOption();
            }
        }

        private void RunChosenOption() // Główna logika wybieranych opcji
        {
            switch (currentOption)
            {
                case 0:   //Show list of entities
                    Console.Clear();
                    var collectionPlayerAndNPC = repositoryPlayer.GetAll();
                    var collectionGm = repositoryGM.GetAll();
                    var collectionNpc = repositoryNPC.GetAll();
                    Console.WriteLine("All entities: ");
                    Console.WriteLine();

                    foreach (var item in collectionPlayerAndNPC)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in collectionGm)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in collectionNpc)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in newAddedNpcs)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in newAddedGMs)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in newAddedPlayers)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
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
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 2:    //Delete entity
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to delete");
                    var input = Console.ReadLine();
                    int.TryParse(input, out int result);
                    var entity = repositoryPlayer.GetById(result);
                    var entity1 = repositoryGM.GetById(result);
                    var entity2 = repositoryGM.GetById(result);

                    if (entity != null)
                    {
                        repositoryPlayer.Remove(entity);
                        repositoryPlayer.Save();
                    }
                    else if (entity1 != null)
                    {
                        repositoryGM.Remove(entity1);
                        repositoryGM.Save();
                    }
                    else if (entity2 != null)
                    {
                        repositoryGM.Remove(entity2);
                        repositoryGM.Save();
                    }
                    else
                    {
                        Console.WriteLine("Entity not found");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 3:   //Find entity
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to display");
                    var input2 = Console.ReadLine();
                    int.TryParse(input2, out int result2);
                    var entity3 = repositoryPlayer.GetById(result2);
                    var entity4 = repositoryGM.GetById(result2);
                    if (entity3 != null)
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
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 4:   //Exit
                    repositoryPlayer.AddBatch(newAddedPlayers);
                    repositoryNPC.AddBatch(newAddedNpcs);
                    repositoryGM.AddBatch(newAddedGMs);

                    repositoryPlayer.SaveToFile(filePath);
                    repositoryGM.SaveToFile(filePath2);
                    repositoryNPC.SaveToFile(filePath3);
                    Environment.Exit(0);
                    break;
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
            string[] entityCreatingOptions = new string[] { "Player", "Npc", "Game Master", "Return" };

            PrintCreatingNewEntitiyMenu();
            ChooseOption(entityCreatingOptions, ref activeOption, PrintCreatingNewEntitiyMenu);
            string? name;
            switch (activeOption)
            {
                case 0: // Player
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedPlayers.Add(new Player(name) { Id = CheckAvailability() });
                    break;
                case 1: // Npc
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedNpcs.Add(new Npc(name) { Id = CheckAvailability() });
                    break;
                case 2: //Game Master
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedGMs.Add(new GameMaster(name) { Id = CheckAvailability() });
                    break;
                case 3:
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
                int result = 1;
                while (true)
                {
                    result = repositoryPlayer.GetFirstFreeId(result); //zweryfikować czy na pewno dobre id wyrzuca teraz po zmianie
                    result = repositoryGM.GetFirstFreeId(result);
                    result = repositoryNPC.GetFirstFreeId(result);
                    foreach (var obj in newAddedPlayers)
                    {
                        if (obj.Id == result)
                        {
                            result = ++result;
                            continue;
                        }
                    }
                    foreach (var obj in newAddedNpcs)
                    {
                        if (obj.Id == result)
                        {
                            result = ++result;
                            continue;
                        }
                    }
                    foreach (var obj in newAddedGMs)
                    {
                        if (obj.Id == result)
                        {
                            result = ++result;
                            continue;
                        }
                    }
                    return result;
                }
            }
        }
    }
}
