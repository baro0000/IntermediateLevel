using GameEntitiesBase;
using GameEntitiesBase.Data;
using GameEntitiesBase.DataProvider;
using GameEntitiesBase.Entities;
using GameEntitiesBase.Repositories;
using GameEntitiesBase.Repositories.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IUserCommunication, MenuGenerator>();
services.AddSingleton<IRepository<Player>, ListRepository<Player>>();
services.AddSingleton<IRepository<Npc>, ListRepository<Npc>>();
services.AddSingleton<IApp, App>();
services.AddSingleton<IDataProvider, DataProvider>();

var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetService<IApp>();
app.Run();