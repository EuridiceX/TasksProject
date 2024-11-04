using TaskManagement.Data.RabitMqProductAPI.Data;
using TaskManagement.Entities;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContextClass _dbContext;
        public TaskRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Create(TaskEntity task)
        {
            var result = _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public IEnumerable<TaskEntity> GetTask()
        {
            return _dbContext.Tasks.ToList();
        }

        public Task Update(TaskStatusEnum status)
        {
            // var result = _dbContext.Tasks.Update(status);
            // _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
