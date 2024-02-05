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
        private int currentOption;
        private string[] menuOptions = { "Show list of entities",
                                                                  "Add new entity",
                                                                  "Delete entity",
                                                                  "Find entity",
                                                                  "Quit" };

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
                repository.ItemAdded += PlayerAdded;
                repository.ItemRemoved += PlayerRemoved;
            }
        }
        void PlayerAdded(object sender, Player player)
        {
            Console.WriteLine($"{player.Name} added to database");
        }
        void PlayerRemoved(object sender, Player player)
        {
            Console.WriteLine($"{player.Name} removed from database");
        }

        private void PrintMainMenu() //wyświetla menu i podświetla aktywną opcję
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Welcome to the game entities database <<<");
            Console.WriteLine();

            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (currentOption == i)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{menuOptions[i],-35}");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(menuOptions[i]);
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
                ChooseOption();
                RunChosenOption();
            }
        }

        private void RunChosenOption() // Główna logika wybieranych opcji
        {
            List<Player> newAddedEntities = new List<Player>();
            string name;
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
                case 1:   //OPCJA 2   --- przerobić na działanie generyczne
                    Console.Clear();
                    while (true)
                    {
                        Console.Write("Set Name: ");
                        newAddedEntities.Add(new Player(name = Console.ReadLine()));
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
                    repository.AddBatch(newAddedEntities);

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

        private void ChooseOption() //Obsługa ruchu strzałek i przycisków
        {
            do
            {
                ConsoleKeyInfo chosenKey = Console.ReadKey();

                if (chosenKey.Key == ConsoleKey.UpArrow)
                {
                    currentOption = (currentOption > 0) ? currentOption - 1 : menuOptions.Length - 1;
                    PrintMainMenu();
                }
                else if (chosenKey.Key == ConsoleKey.DownArrow)
                {
                    currentOption = (currentOption < menuOptions.Length - 1) ? currentOption + 1 : currentOption = 0;
                    PrintMainMenu();
                }
                else if (chosenKey.Key == ConsoleKey.Escape)
                {
                    currentOption = menuOptions.Length - 1;
                    break;
                }
                else if (chosenKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
        }
    }
}
