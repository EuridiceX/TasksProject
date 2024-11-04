using TaskManagement.Entities;

namespace TaskManagement.Repositories
{
    public interface ITaskRepository
    {
        public IEnumerable<TaskEntity> GetTask();
        public Task Create(TaskEntity task);
        public Task Update(TaskStatusEnum status);
    }
}
