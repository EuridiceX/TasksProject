using TaskManagement.Data;
using TaskManagement.Data.Repositories;
using TaskManagement.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IServiceBusHandler, ServiceBusProducer>();

builder.Services.AddHostedService<ServiceBusConsumer>();

builder.Services.AddSingleton<RabbitConnectionFactory>();
builder.Services.AddDbContext<DbContextClass>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
