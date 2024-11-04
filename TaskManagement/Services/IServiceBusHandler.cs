namespace TaskManagement.Services
{
    public interface IServiceBusHandler
    {
        public Task SendMessage<T>(T model);
    }
}
