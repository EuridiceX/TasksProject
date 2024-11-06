using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Common.ServiceBus
{
    public class RabbitConnectionFactory : IDisposable
    {
        private  IConnection _connection;
        private bool _disposed;
        private const int MaxRetries = 3;


        public async Task<IConnection> GetConnection()
        {
            if (_connection != null)
            { 
                return _connection; 
            }
            _connection = await CreateConnectionWithRetry();

            return _connection;
        }

        private async Task<IConnection> CreateConnectionWithRetry()
        {
            var attempts = 0;
            var factory = new ConnectionFactory { HostName = "localhost" };

            while (attempts < MaxRetries)
            {
                try
                {
                    _connection = factory.CreateConnection();

                    return _connection;
                }
                catch (BrokerUnreachableException)
                {
                    Console.WriteLine($"Connection failed. Retrying to connect...");

                    attempts++;

                    await Task.Delay(TimeSpan.FromSeconds(3));

                    if (attempts >= MaxRetries)
                    {
                        Console.WriteLine("Max retries reached. Could not connect to RabbitMQ.");
                        throw;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(3));

                }
            }
            throw new InvalidOperationException("Failed to create a connection to RabbitMQ.");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connection.Close();
                _connection.Dispose();
                _disposed = true;
            }
        }
    }

}
