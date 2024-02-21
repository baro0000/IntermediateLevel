using GameEntitiesBase.Components.DataProvider;
using GameEntitiesBase.Data.Entities;
using GameEntitiesBase.Data.Repositories;
using System.Numerics;
using System;

namespace GameEntitiesBase
{
    public class App : IApp
    {
        IUserCommunication _userCommunication;
        IDataProvider _dataProvider;
        IRepository<Player> _playersRepository;
        IRepository<Npc> _npcRepository;

        private string filePlayer = "savePlayer.txt";
        private string fileNpc = "saveNPC.txt";
        private string fileEventLog = "EventLog.txt";

        public App(IUserCommunication menu, IDataProvider dataProvider, IRepository<Player> repositoryPlayer, IRepository<Npc> repositoryNPC)
        {
            _userCommunication = menu;
            _dataProvider = dataProvider;
            _playersRepository = repositoryPlayer;
            _npcRepository = repositoryNPC;

            repositoryPlayer.ItemAdded += ObjectAdded!;
            repositoryPlayer.ItemAdded += WriteToEventLogObjAdded!;
            repositoryPlayer.ItemRemoved += ObjectRemoved!;
            repositoryPlayer.ItemRemoved += WriteToEventLogObjRemoved!;

            repositoryNPC.ItemAdded += ObjectAdded!;
            repositoryNPC.ItemAdded += WriteToEventLogObjAdded!;
            repositoryNPC.ItemRemoved += ObjectRemoved!;
            repositoryNPC.ItemRemoved += WriteToEventLogObjRemoved!;
        }

        public void Run(bool isOfflineActivated)
        {
            Console.Clear();
            Random _random = new Random();
            Npc npc = new Npc() { Name = "Adam"};
            Player player = new Player() { Name = "Challenger"};
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

            //if (_playersRepository.CountObjects() == 0 && _npcRepository.CountObjects() == 0)
            //{
            //    LoadEntitiesFromFiles();
            //    if (_playersRepository.CountObjects() == 0 && _npcRepository.CountObjects() == 0)
            //    {
            //        LoadEntitiesFromProvider();
            //    }
            //}
            //_userCommunication.MainMenu(isOfflineActivated);

            //if (isOfflineActivated)
            //{
            //    Environment.Exit(0);
            //}
            //SaveRepositoriesToFiles();
        }

        private void ObjectAdded(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} added to database");
        }

        private void ObjectRemoved(object sender, EntityBase obj)
        {
            Console.WriteLine($"{obj.Name} removed from database");
        }

        private void WriteToEventLogObjAdded(object sender, EntityBase obj)
        {
            using (var writer = File.AppendText(fileEventLog))
            {
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : ID: {obj.Id} object {obj.Name} has been added");
            }
        }

        private void WriteToEventLogObjRemoved(object sender, EntityBase obj)
        {
            if (File.Exists(fileEventLog))
            {
                using (var writer = File.AppendText(fileEventLog))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : ID: {obj.Id} object {obj.Name} has been removed");
                }
            }
        }

        private void LoadEntitiesFromProvider()
        {
            foreach (var player in _dataProvider.ProvidePlayers())
            {
                _playersRepository.Add(new Player()
                {
                    Id = player.Id,
                    Sex = player.Sex,
                    Name = player.Name,
                    Race = player.Race,
                    Profession = player.Profession,
                    Stats = player.Stats,
                    Level = player.Level
                });
            }
            foreach (var npc in _dataProvider.ProvideNpcs())
            {
                _npcRepository.Add(new Npc()
                {
                    Id = npc.Id,
                    Sex = npc.Sex,
                    Name = npc.Name,
                    Race = npc.Race,
                    Profession = npc.Profession,
                    Stats = npc.Stats,
                    Level = npc.Level
                });
            }
            _playersRepository.Save();
            _npcRepository.Save();
        }

        private void LoadEntitiesFromFiles()
        {
            _npcRepository.LoadFromFile(fileNpc);
            _playersRepository.LoadFromFile(filePlayer);
        }

        private void SaveRepositoriesToFiles()
        {
            _playersRepository.SaveToFile(filePlayer);
            _npcRepository.SaveToFile(fileNpc);
        }
    }
}
