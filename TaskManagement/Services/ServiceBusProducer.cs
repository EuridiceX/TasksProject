using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace TaskManagement.Services
{
    public class ServiceBusProducer : IServiceBusHandler
    {
        private readonly ILogger<ServiceBusProducer> _logger;
        private readonly IConnection _connection;
        private const string QueueName = "hello";


        public ServiceBusProducer(ILogger<ServiceBusProducer> logger, RabbitConnectionFactory factory)
        {
            _logger = logger;
            _connection = factory.GetConnection();
        }

        public Task SendMessage<T>(T model)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(model);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: QueueName,
                                 basicProperties: null,
                                 body: body);

            _logger.LogInformation($" [x] Sent {json}");

            return Task.CompletedTask;
        }
    }
}
