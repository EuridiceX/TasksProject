using Common.ServiceBus;
using CommonLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.Entities;
using TaskManagement.Data.Repositories;

namespace TaskManagement.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly IServiceBusHandler _serviceBus;

        public TaskController(ILogger<TaskController> logger, 
            ITaskRepository taskRepository, IServiceBusHandler serviceBus)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _serviceBus = serviceBus;
        }


        [HttpGet("GetTasks")]
        public async Task<IEnumerable<TaskEntity>> Get()
        {
            return await _taskRepository.GetTasks();
        }

        [HttpPost("Create")]
        public async Task Create([FromBody] TaskModel taskmodel)
        {
            var createAction = new ActionModel<TaskModel>
            {
                ActionType = ActionType.CreateEntity,
                Data = taskmodel
            };

            await _serviceBus.SendMessage(createAction, QueueName.CommandQueue);
        }

        [HttpPut("UpdateStatus")]
        public async Task Update([FromBody] UpdateModel updateModel)
        {
            var updateAction = new ActionModel<TaskModel>
            {
                ActionType = ActionType.UpdateStatus,
                Data = new TaskModel { Status = updateModel.Status, Id = updateModel.Id }
            };

            await _serviceBus.SendMessage(updateAction, QueueName.CommandQueue);

        }
    }

}
