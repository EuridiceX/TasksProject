using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.Entities;
using TaskManagement.Data.Repositories;
using TaskManagement.Services;

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
        public async Task Create([FromBody] TaskEntity taskEntity)
        {
             await _taskRepository.Create(taskEntity);
        }

        [HttpPut("UpdateStatus")]
        public async  Task Update([FromBody] UpdateModel updateModel)
        {
            var updateAction = new ActionModel<UpdateModel>
            {
                Action = ActionType.UpdateStatus,
                Data = updateModel
            };

            await _serviceBus.SendMessage(updateAction);


            await _taskRepository.Update(updateModel.Status, updateModel.Id);

        }
    }

    public class ActionModel<T>
    {
        public ActionType Action { get; set; }
        public T Data { get; set; }
    }
    public enum ActionType
    {
        UpdateStatus
    }

    public class UpdateModel
    {
        public int Id { get; set; }
        public TaskStatusEnum Status { get; set; }
    }

}
