using Common.ServiceBus;
using CommonLibrary.Models;
using ConsumerService.Data.Repositories;
using Newtonsoft.Json;
using TaskManagement.Data.Entities;

namespace ConsumerService
{
    public class MessageProcessor: ServiceBusHandler
    {
        private ITaskRepository _taskRepository;

        public MessageProcessor(ITaskRepository taskRepository, RabbitConnectionFactory factory):base(factory)  
        {
            _taskRepository = taskRepository;
        }

        public override void HandleMessage(string message)
        {
            Console.WriteLine($" [x] Received {message}");

            var action = JsonConvert.DeserializeObject<ActionModel<TaskModel>>(message);

            Handle(action);
        }
       
        private void Handle(ActionModel<TaskModel> message)
        {
            switch (message.Action)
            {
                case ActionType.UpdateStatus:

                    _taskRepository.Update(message.Data.Status.Value, message.Data.Id.Value);

                    this.SendMessage("Update Completed", "Status");

                    break;

                case ActionType.CreateEntity:
                    var createEntity = new TaskEntity
                    {
                        AssignedTo = message.Data.AssignedTo,
                        Description = message.Data.Description,
                        Name = message.Data.Name,
                        Status = (int)message.Data.Status.Value,
                    };

                    _taskRepository.Create(createEntity);

                    this.SendMessage("Create Completed", "Status");

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(message.Action), "Unknown action type.");

            }

        }
    }
}
