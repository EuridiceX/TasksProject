using Common.ServiceBus;
using ConsumerService;
using ConsumerService.Data;
using ConsumerService.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static async Task Main()
    {
        var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<RabbitConnectionFactory>()
            .AddSingleton<ServiceBusHandler, MessageProcessor>()
            .AddSingleton<MessageProcessor>()
            .AddDbContext<DbContextClass>(options =>
                    options.UseSqlServer(config.GetConnectionString("DBConnection")))
            .AddTransient<ITaskRepository, TaskRepository>()
            .BuildServiceProvider();


            var serviceBusHandler = serviceProvider.GetRequiredService<ServiceBusHandler>();

            await serviceBusHandler.StartListening("hello");

            Console.WriteLine("Press [Enter] to stop and exit.");

            Console.ReadLine();

            serviceBusHandler.CleanUp();

    }
}