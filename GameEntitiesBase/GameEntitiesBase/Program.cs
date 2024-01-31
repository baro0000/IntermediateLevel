using GameEntitiesBase.Data;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;

var repository = new SqlEntityRepository<Player>(new GameEntitiesBaseDbContext());
AddPlayers(repository);
AddGameMasters(repository);
PrintAllToConsole(repository);

static void AddPlayers(IRepository<Player> repository)
{
    repository.Add(new Player() { Name = "Adam" });
    repository.Add(new Player() { Name = "Gosia" });
    repository.Add(new Player() { Name = "Kuba" });
    repository.Save();
}

static void AddGameMasters(IWriteRepository<Npc> repository)
{
    repository.Add(new Npc() { Name = "Zosia" });
    repository.Add(new Npc() { Name = "Ewa" });
    repository.Add(new Npc() { Name = "Filip" });
    repository.Save();
}
static void PrintAllToConsole(IReadRepository<IEntity> repository)
{
    var collection = repository.GetAll();
    foreach (var item in collection)
    {
        Console.WriteLine(item);
    }
}