using GameEntitiesBase.Data.Entities;
using static GameEntitiesBase.Data.Entities.Available;

namespace GameEntitiesBase.Components.DataProvider
{
    public class DataProvider : IDataProvider
    {
        public Player CreateNewPlayer()
        {
            Gender gender = ChooseGender();

            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            Race race = ChooseRace();

            Profession profession = ChooseProfession();

            Statistics statistics = GenerateStatistics(race);

            return new Player(gender, name, race, profession, statistics, 1);
        }

        public Npc CreateNewNpc(int level = 1)
        {
            Gender gender = ChooseGender();

            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            Race race = ChooseRace();

            Profession profession = ChooseProfession();

            Statistics statistics = GenerateStatistics(race);

            return new Npc(gender, name, race, profession, statistics, level);
        }

        private Statistics GenerateStatistics(Race race)
        {
            Random rand = new Random();
            Console.WriteLine("Now we have to fill up your character statistics. Press any key to roll the dice");
            Console.ReadKey();
            var score = rand.Next(20, 36);
            Console.WriteLine($"Your score is: {score}. Now your abilities are:");
            Console.WriteLine();
            Console.WriteLine("Strenght - determines how strong you hit. Provides bonus to attack, and bonus to damage.");
            Console.WriteLine("Agility - determines how fast you are. Provides bonus to dodge, and bonus to defence.");
            Console.WriteLine();
            Console.WriteLine("Choose value of Strength, and Agility will be filled automaticly with remaining points.");
            Console.WriteLine("Remember: each ability cant be lower than 10, and higher than 18");
            Console.WriteLine();
            Console.WriteLine("Choose your strenght value: ");
            int strengthValue, agilityValue;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result < 10 || result > 18 || (score - result) > 18)
                    {
                        Console.WriteLine("Wrong input! Remember: each ability cant be lower than 10, and higher than 18!");
                        Console.WriteLine($"Points to display: {score}");
                        Console.WriteLine("Choose again your strenght value: ");
                    }
                    else
                    {
                        strengthValue = result;
                        agilityValue = score - result;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input! Please provide correct numbers!");
                }
            } while (true);
            Console.WriteLine($"str: {strengthValue} agil: {agilityValue}");
            return new Statistics(strengthValue, agilityValue, race);
        }

        private Profession ChooseProfession()
        {
            Console.WriteLine("Choose profession: ");
            Console.WriteLine("1. Barbarian");
            Console.WriteLine("2. Knight");

            Profession profession;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result == 1)
                    {
                        profession = Profession.Barbarian;
                        break;
                    }
                    else if (result == 2)
                    {
                        profession = Profession.Knight;
                        break;
                    }
                    else
                        Console.WriteLine("Wrong input! Try again");
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again");
                }
            } while (true);
            return profession;
        }

        private Race ChooseRace()
        {
            Console.WriteLine("Choose race: ");
            Console.WriteLine("1. Human");
            Console.WriteLine("2. Dwarf");
            Console.WriteLine("3. Elf");

            Race race;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result == 1)
                    {
                        race = Race.Human;
                        break;
                    }
                    else if (result == 2)
                    {
                        race = Race.Dwarf;
                        break;
                    }
                    else if (result == 3)
                    {
                        race = Race.Elf;
                        break;
                    }
                    else
                        Console.WriteLine("Wrong input! Try again");
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again");
                }
            } while (true);
            return race;
        }

        private Gender ChooseGender()
        {
            Console.WriteLine("Choose gender:");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            Gender gender;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result == 1)
                    {
                        gender = Gender.Male;
                        break;
                    }
                    else if (result == 2)
                    {
                        gender = Gender.Female;
                        break;
                    }
                    else
                        Console.WriteLine("Wrong input! Try again");
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again");
                }
            } while (true);

            return gender;
        }

        public List<Player> ProvidePlayers()
        {
            List<Player> list = new()
            {
                new Player(Gender.Male, "Adam", Race.Human, Profession.Knight, new Statistics(14, 12, Race.Human), 1),
                new Player(Gender.Male, "Mariusz", Race.Dwarf, Profession.Knight, new Statistics(16, 13, Race.Dwarf), 1),
                new Player(Gender.Male, "Jakub", Race.Human, Profession.Barbarian,  new Statistics(17, 12, Race.Human), 1),
            };
            return list;
        }

        public List<Npc> ProvideNpcs()
        {
            Gender gender = Gender.Male;
            List<Npc> list = new()
            {
                new Npc(gender, "Noob", Race.Human, Profession.Knight, new Statistics(18, 14, Race.Human), 1),
                new Npc(gender, "Noobier", Race.Dwarf, Profession.Knight, new Statistics(14, 14, Race.Dwarf), 2),
                new Npc(gender, "The Noobiest", Race.Elf, Profession.Barbarian, new Statistics(10, 10, Race.Elf), 3),
            };
            return list;
        }
    }
}
