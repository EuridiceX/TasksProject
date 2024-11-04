using RabbitMQ.Client;

namespace TaskManagement.Services
{
    public class RabbitConnectionFactory : IDisposable
    {
        private readonly IConnection _connection;
        private bool _disposed;

        public RabbitConnectionFactory()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
        }
        public IConnection GetConnection()
        {
            return _connection;
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
