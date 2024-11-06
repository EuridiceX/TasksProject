using Common.ServiceBus;
using CommonLibrary.Models;
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
               .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
               .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<RabbitConnectionFactory>()
            .AddSingleton<ServiceBusHandler, CommandHandlerService>()
            .AddSingleton<CommandHandlerService>()
            .AddDbContext<DbContextClass>(options =>
                    options.UseSqlServer(config.GetConnectionString("DBConnection")))
            .AddTransient<ITaskRepository, TaskRepository>()
            .BuildServiceProvider();


            var actionHandler = serviceProvider.GetRequiredService<CommandHandlerService>();

            await actionHandler.StartListening(QueueName.CommandQueue);

            Console.WriteLine("Press [Enter] to stop and exit.");

            Console.ReadLine();

            actionHandler.CleanUp();

    }
}