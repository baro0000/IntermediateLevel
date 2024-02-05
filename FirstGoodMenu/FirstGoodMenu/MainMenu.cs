namespace FirstGoodMenu
{
    public class MainMenu
    {
        private string[] listOfOptions = { "1: Option 1", "2: Option 2", "3: Option 3", "4: Exit" };
        int activeOptionInMenu;

        public MainMenu()
        {
            activeOptionInMenu = 0;
        }

        private void PrintMainMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Welcome to first good-looking console Main Menu project.\nPlease choose option:");
            Console.WriteLine();

            for (int i = 0; i < listOfOptions.Length; i++)
            {
                if (i == activeOptionInMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0, -35}", listOfOptions[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(listOfOptions[i]);
                }
            }

            Console.WriteLine("To exit choose 4, or press Escape");
        }

        public void RunMainMenu()
        {
            while (true)
            {
                Console.Title = "First Main Menu";
                Console.CursorVisible = false;

                PrintMainMenu();
                OptionChoosing();
                RunChosenOption();
            }
        }

        private void RunChosenOption()
        {
            switch (activeOptionInMenu)
            {
                case 0: Console.Clear(); Console.WriteLine("Opcja pierwsza"); Console.WriteLine("Press any key to continue"); Console.ReadLine(); break;
                case 1: Console.Clear(); Console.WriteLine("Opcja druga"); Console.WriteLine("Press any key to continue"); Console.ReadLine(); break;
                case 2: Console.Clear(); Console.WriteLine("Opcja trzecia"); Console.WriteLine("Press any key to continue"); Console.ReadLine(); break;
                case 3: Environment.Exit(0); break;
            }
        }

        private void OptionChoosing()
        {
            do
            {
                ConsoleKeyInfo chosenKey = Console.ReadKey();
                if (chosenKey.Key == ConsoleKey.UpArrow)
                {
                    activeOptionInMenu = (activeOptionInMenu > 0) ? activeOptionInMenu - 1 : listOfOptions.Length - 1;
                    PrintMainMenu();
                }
                else if (chosenKey.Key == ConsoleKey.DownArrow)
                {
                    activeOptionInMenu = (activeOptionInMenu + 1) % listOfOptions.Length;
                    PrintMainMenu();
                }
                else if (chosenKey.Key == ConsoleKey.Escape)
                {
                    activeOptionInMenu = listOfOptions.Length - 1;
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
