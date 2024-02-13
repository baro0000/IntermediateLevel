using GameEntitiesBase.DataProvider;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;

namespace GameEntitiesBase
{
    public class App : IApp
    {
        IUserCommunication _menuGenerator;
        IDataProvider _dataProvider;
        IRepository<Player> _playersRepository;
        IRepository<Npc> _npcRepository;

        private string filePlayer = "savePlayer.txt";
        private string fileNpc = "saveNPC.txt";
        private string fileEventLog = "EventLog.txt";

        public App(IUserCommunication menu, IDataProvider dataProvider, IRepository<Player> repositoryPlayer, IRepository<Npc> repositoryNPC)
        {
            _menuGenerator = menu;
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

        public void Run()
        {
            LoadEntitiesFromFiles();

            if (_playersRepository.Count() == 0 && _npcRepository.Count() == 0)
            {
                LoadEntitiesFromProvider();
            }

            _menuGenerator.MainMenu();

            SaveRepositoriesToFiles();
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
                _playersRepository.Add(player);
            }
            foreach (var npc in _dataProvider.ProvideNpcs())
            {
                _npcRepository.Add(npc);
            }
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
