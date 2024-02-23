using GameEntitiesBase;
using GameEntitiesBase._2_ApplicationServices;
using GameEntitiesBase.Components.DataProvider;
using GameEntitiesBase.Data;
using GameEntitiesBase.Data.Entities;
using GameEntitiesBase.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var connectionString = "Data Source=DESKTOP-CBK7MCF\\SQLEXPRESS;Initial Catalog=TestStorage;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
bool offlineMode = false;
var services = new ServiceCollection();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IApp, App>();
services.AddSingleton<IDataProvider, DataProvider>();
services.AddSingleton<IFightMenager, FightMenager>();

var dbContextOptionsBuilder = new DbContextOptionsBuilder<GameEntitiesBaseDbContext>();
dbContextOptionsBuilder.UseSqlServer(connectionString);
CheckConnectionToServerOrRunOffline();

services.AddDbContext<GameEntitiesBaseDbContext>(options => options
    .UseSqlServer(connectionString));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run(offlineMode);

void CheckConnectionToServerOrRunOffline()
{
    Console.WriteLine("Checking connection to server...");

    bool isConnectionPossible = false;
    using (var dbContext = new GameEntitiesBaseDbContext(dbContextOptionsBuilder.Options))
    {
        try
        {
            isConnectionPossible = dbContext.Database.CanConnect();
        }
        catch (Exception ex)
        {
        }
    }
    if (isConnectionPossible)
    {
        services.AddSingleton<IRepository<Player>, SqlEntityRepository<Player>>();
        services.AddSingleton<IRepository<Npc>, SqlEntityRepository<Npc>>();
    }
    else
    {
        offlineMode = true;
        services.AddSingleton<IRepository<Player>, ListRepository<Player>>();
        services.AddSingleton<IRepository<Npc>, ListRepository<Npc>>();
    }
}
//#######################################################################################
//var services = new ServiceCollection();
//services.AddSingleton<IUserCommunication, UserCommunication>();
//services.AddSingleton<IApp, App>();
//services.AddSingleton<IDataProvider, DataProvider>();
//services.AddDbContext<GameEntitiesBaseDbContext>(options => options
//.UseSqlServer("Data Source=DESKTOP-CBK7MCF\\SQLEXPRESSAAA;Initial Catalog=GameEntityBase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

//var serviceProvider = services.BuildServiceProvider();

//var dbContext = serviceProvider.GetService<GameEntitiesBaseDbContext>();

//try
//{
//    bool isConnectionPossible = dbContext.Database.CanConnect();
//    if (isConnectionPossible)
//    {
//        services.AddSingleton<IRepository<Player>, SqlEntityRepository<Player>>();
//        services.AddSingleton<IRepository<Npc>, SqlEntityRepository<Npc>>();
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Database connection error {ex.Message}");
//    Console.WriteLine("Start aplication in offline mode? Press ESC to abort or any key to continue");
//    var input = Console.ReadKey();
//    if(input.Key == ConsoleKey.Escape)
//    {
//        Environment.Exit(0);
//    }
//    services.AddSingleton<IRepository<Player>, ListRepository<Player>>();
//    services.AddSingleton<IRepository<Npc>, ListRepository<Npc>>();
//}

//var app = serviceProvider.GetService<IApp>();
//app.Run();
