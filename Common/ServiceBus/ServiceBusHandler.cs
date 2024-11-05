using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.ServiceBus
{
    public class ServiceBusHandler : IServiceBusHandler, IDisposable
    {
        private readonly IConnection _connection;
        private IModel _channel;

        public ServiceBusHandler(RabbitConnectionFactory factory)
        {
            _connection = factory.GetConnection();
        }

        public Task StartListening(string queueName)
        { 
            _channel = _connection.CreateModel();

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

            return Task.CompletedTask;
        }

        public virtual void HandleMessage(string message)
        {
            Console.WriteLine($" [x] Received {message}");
        }

        public Task SendMessage<T>(T model, string queueName)
        {
            using var channel = _connection.CreateModel();

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

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Close();
        }

        public void CleanUp()
        {
            Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}
