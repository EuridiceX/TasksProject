using TaskManagement.Data.Entities;

namespace TaskManagement.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskEntity>> GetTasks();
    }
}
