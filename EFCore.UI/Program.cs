using EFCore.DAL;
using EFCore.Service;
using EFCore.Shared.Interfaces;
using EFCore.UI;
using Microsoft.Extensions.DependencyInjection;

new ServiceCollection()
    .AddScoped<DataContext>()
    .AddSingleton<IProductService, ProductService>()
    .AddSingleton<ICategoryService, CategoryService>()
    .AddSingleton<IOrderService, OrderService>()
    .AddSingleton<IClientService, ClientService>()
    .AddSingleton<Application>()
    .BuildServiceProvider()
    .GetRequiredService<Application>()
    .Run();
