using System.Collections.Concurrent;

namespace Common.ServiceBus
{
    public interface IServiceBusHandler
    {
        public Task SendMessage<T>(T model, string queueName);

        public Task StartListening(string queueName);
        void CleanUp();
    }
}
