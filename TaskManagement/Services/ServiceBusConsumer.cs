
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace TaskManagement.Services
{
    public class ServiceBusConsumer : BackgroundService
    {
        private readonly ILogger<ServiceBusConsumer> _logger;
        private IModel _channel;
        private const string QueueName = "hello";


        public ServiceBusConsumer(ILogger<ServiceBusConsumer> logger, RabbitConnectionFactory factory)
        {
            _logger = logger;
            var connection = factory.GetConnection();
            _channel = connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($" [x] Received {message}");
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            base.Dispose();
        }
    }
}
