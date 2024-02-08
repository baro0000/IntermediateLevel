using GameEntitiesBase.Data;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;
using GameEntitiesBase.Repositories.Extensions;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace GameEntitiesBase
{
    public class MenuGenerator
    {
        private string filePath = "savePlayerAndNpc.txt";
        private string filePath2 = "saveGM.txt";
        private string filePath3 = "saveNPC.txt";
        private string filePath4 = "EventLog.txt";

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
            repositoryPlayer.ItemAdded += WriteToEventLogObjAdded;
            repositoryPlayer.ItemRemoved += ObjectRemoved;
            repositoryPlayer.ItemRemoved += WriteToEventLogObjRemoved;

            repositoryGM.ItemAdded += ObjectAdded;
            repositoryGM.ItemAdded += WriteToEventLogObjAdded;
            repositoryGM.ItemRemoved += ObjectRemoved;
            repositoryGM.ItemRemoved += WriteToEventLogObjRemoved;

            repositoryNPC.ItemAdded += ObjectAdded;
            repositoryNPC.ItemAdded += WriteToEventLogObjAdded;
            repositoryNPC.ItemRemoved += ObjectRemoved;
            repositoryNPC.ItemRemoved += WriteToEventLogObjRemoved;
        }

        void ObjectAdded(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} added to database");
        }
        void ObjectRemoved(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} removed from database");
        }
        void WriteToEventLogObjAdded(object sender, EntityBase obj)
        {

            using (var writer = File.AppendText(filePath4))
            {
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : ID: {obj.Id} object {obj.Name} has been added");
            }

        }
        void WriteToEventLogObjRemoved(object sender, EntityBase obj)
        {
            if (File.Exists(filePath4))
            {
                using (var writer = File.AppendText(filePath4))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : ID: {obj.Id} object {obj.Name} has been removed");
                }
            }
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

                    var sortedList = PrintEntitiesFromAllBases();

                    Console.WriteLine("All entities: ");
                    Console.WriteLine();

                    foreach (var item in sortedList)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to return...");
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

        private List<EntityBase> PrintEntitiesFromAllBases()
        {
            List<EntityBase> list = new List<EntityBase>();
            list.AddRange(repositoryPlayer.GetAll());
            list.AddRange(repositoryGM.GetAll());
            list.AddRange(repositoryNPC.GetAll());
            list.AddRange(newAddedNpcs);
            list.AddRange(newAddedGMs);
            list.AddRange(newAddedPlayers);

            return list.OrderBy(obj => obj.Id).ToList(); ;
        }

        private void DeleteItem(int id)
        {
            var entity1 = repositoryPlayer.GetById(id);
            var entity2 = repositoryGM.GetById(id);
            var entity3 = repositoryNPC.GetById(id);
            var entity4 = newAddedPlayers.SingleOrDefault(obj => obj.Id == id);
            var entity5 = newAddedNpcs.SingleOrDefault(obj => obj.Id == id);
            var entity6 = newAddedGMs.SingleOrDefault(obj => obj.Id == id);

            if (entity1 != null)
            {
                repositoryPlayer.Remove(entity1);
                repositoryPlayer.Save();
            }
            else if (entity2 != null)
            {
                repositoryGM.Remove(entity2);
                repositoryGM.Save();
            }
            else if (entity3 != null)
            {
                repositoryNPC.Remove(entity3);
                repositoryGM.Save();
            }
            else if (entity4 != null)
            {
                newAddedPlayers.Remove(entity4);
                Console.WriteLine($"{entity4.Name} removed from database");
            }
            else if (entity5 != null)
            {
                newAddedNpcs.Remove(entity5);
                Console.WriteLine($"{entity5.Name} removed from database");
            }
            else if (entity6 != null)
            {
                newAddedGMs.Remove(entity6);
                Console.WriteLine($"{entity6.Name} removed from database");
            }
            else
            {
                Console.WriteLine("Entity not found");
            }
        }
        private void FindAndDisplay(int id)
        {
            var entity1 = repositoryPlayer.GetById(id);
            var entity2 = repositoryGM.GetById(id);
            var entity3 = repositoryNPC.GetById(id);
            var entity4 = newAddedPlayers.SingleOrDefault(obj => obj.Id == id);
            var entity5 = newAddedNpcs.SingleOrDefault(obj => obj.Id == id);
            var entity6 = newAddedGMs.SingleOrDefault(obj => obj.Id == id);

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
            else if (entity5 != null)
            {
                Console.WriteLine(entity5);
            }
            else if (entity6 != null)
            {
                Console.WriteLine(entity6);
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
                foreach (var obj in newAddedGMs)
                {
                    unavailableIds.Add(obj.Id);
                }
                foreach (var entity in repositoryPlayer.GetAll())
                {
                    unavailableIds.Add(entity.Id);
                }
                foreach (var entity in repositoryGM.GetAll())
                {
                    unavailableIds.Add(entity.Id);
                }
                foreach (var entity in repositoryNPC.GetAll())
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
