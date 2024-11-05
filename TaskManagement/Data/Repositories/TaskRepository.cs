using Common;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Entities;

namespace TaskManagement.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContextClass _dbContext;
        public TaskRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasks()
        {
            return await _dbContext.Tasks.AsNoTracking().ToListAsync();
        }

    }
}
