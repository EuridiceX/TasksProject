using Common.ServiceBus;
using TaskManagement.Data;
using TaskManagement.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<IServiceBusHandler, ServiceBusHandler>();

builder.Services.AddSingleton<RabbitConnectionFactory>();
builder.Services.AddDbContext<DbContextClass>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


var serviceBusHandler = app.Services.GetRequiredService<IServiceBusHandler>();

await serviceBusHandler.StartListening("Status");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
