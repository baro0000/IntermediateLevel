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
        private string filePath = "save.txt";
        SqlEntityRepository<Player> repository = new SqlEntityRepository<Player>(new GameEntitiesBaseDbContext());
        List<Player> newAddedPLayers = new List<Player>();
        List<Npc> newAddedNpcs = new List<Npc>();
        List<GameMaster> newAddedGMs = new List<GameMaster>();
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
            if (File.Exists(filePath))
            {
                using (var reader = File.OpenText(filePath))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var obj = JsonSerializer.Deserialize<Player>(line);
                        repository.Add(obj);
                        line = reader.ReadLine();
                    }
                    repository.Save();
                }
                repository.ItemAdded += ObjectAdded;
                repository.ItemRemoved += ObjectRemoved;
            }
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
                case 0:   //OPCJA 1
                    Console.Clear();
                    var collection = repository.GetAll();
                    Console.WriteLine("All entities: ");
                    Console.WriteLine();

                    foreach (var item in collection)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 1:   //OPCJA 2  
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
                    repository.AddBatch(newAddedPLayers);
                    repository.AddBatch(newAddedNpcs);

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 2:    //OPCJA 3
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to delete");
                    var input = Console.ReadLine();
                    int.TryParse(input, out int result);
                    var entity = repository.GetById(result);
                    if (entity != null)
                    {
                        repository.Remove(entity);
                        repository.Save();
                    }
                    else
                    {
                        Console.WriteLine("Entity not found");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 3:   //OPCJA 4
                    Console.Clear();
                    Console.WriteLine("Input Id of entity you want to display");
                    var input2 = Console.ReadLine();
                    int.TryParse(input2, out int result2);
                    var entity2 = repository.GetById(result2);
                    if (entity2 != null)
                    {
                        Console.WriteLine(entity2);
                    }
                    else
                    {
                        Console.WriteLine("Entity not found");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case 4:   //Wyjście
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    using (var writer = File.AppendText(filePath))
                    {
                        foreach (var obj in repository.GetAll())
                        {
                            writer.WriteLine(JsonSerializer.Serialize(obj));
                        }
                    }
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
                case 0:
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedPLayers.Add(new Player(name) { Id = CheckAvailability()});
                    break;
                case 1:
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedNpcs.Add(new Npc(name) { Id = CheckAvailability() });
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Set Name: ");
                    name = Console.ReadLine();
                    newAddedGMs.Add(new GameMaster(name) { Id = CheckAvailability() });
                    break;
                case 4:
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
                    result = repository.GetFirstFreeId(result);
                    foreach (var obj in newAddedPLayers)
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
                    return result;
                }
            }
        }
    }
}
