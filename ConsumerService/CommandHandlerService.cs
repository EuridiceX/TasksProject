using Common.ServiceBus;
using CommonLibrary.Models;
using ConsumerService.Data.Repositories;
using Newtonsoft.Json;
using TaskManagement.Data.Entities;

namespace ConsumerService
{
    public class CommandHandlerService : ServiceBusHandler
    {
        private readonly ITaskRepository _taskRepository;

        public CommandHandlerService(ITaskRepository taskRepository, RabbitConnectionFactory factory) : base(factory)
        {
            _taskRepository = taskRepository;
        }

        public override async Task HandleMessage(string message)
        {
            Console.WriteLine($" [x] Received {message}");

            var action = JsonConvert.DeserializeObject<ActionModel<TaskModel>>(message);

            await Handle(action);
        }

        private async Task Handle(ActionModel<TaskModel> message)
        {
            switch (message.ActionType)
            {
                case ActionType.UpdateStatus:
                   await UpdateTaskStatus(message);
                    break;

                case ActionType.CreateEntity:
                    await CreateTaskEntity(message);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(message.ActionType), "Unknown action type.");
            }
        }

        private async Task UpdateTaskStatus(ActionModel<TaskModel> message)
        {
            await  _taskRepository.Update(message.Data.Status.Value, message.Data.Id.Value);
            await  SendMessage(WriteStatus.UpdateSuccessFul.ToString(), QueueName.StatusQueue);
        }

        private async Task CreateTaskEntity(ActionModel<TaskModel> message)
        {
            var createEntity = new TaskEntity
            {
                AssignedTo = message.Data.AssignedTo,
                Description = message.Data.Description,
                Name = message.Data.Name,
                Status = (int)message.Data.Status.Value,
            };

            await _taskRepository.Create(createEntity);
            await SendMessage(WriteStatus.CreateSuccessful.ToString(), QueueName.StatusQueue);
        }

        public void CleanUp()
        {
            Dispose();
        }
    }
}
