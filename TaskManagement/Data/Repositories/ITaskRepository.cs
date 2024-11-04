using TaskManagement.Data.Entities;

namespace TaskManagement.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskEntity>> GetTasks();
        public Task Create(TaskEntity task);
        public Task Update(TaskStatusEnum status, int updateId);
    }
}
