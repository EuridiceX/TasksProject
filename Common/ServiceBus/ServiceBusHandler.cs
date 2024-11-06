using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.ServiceBus
{
    public class ServiceBusHandler : IServiceBusHandler, IDisposable
    {
        private readonly RabbitConnectionFactory _factory;
        private IModel _channel;

        public ServiceBusHandler(RabbitConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task StartListening(string queueName)
        {
            var connection = await _factory.GetConnection();

            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                HandleMessage(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        }

        public virtual Task HandleMessage(string message)
        {
            Console.WriteLine($" [x] Received {message}");

            return Task.CompletedTask;
        }

        public async Task SendMessage<T>(T model, string queueName)
        {
            var connection = await _factory.GetConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(model);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

        }

        public void Dispose()
        {
            _channel.Close();
        }
    }
}
