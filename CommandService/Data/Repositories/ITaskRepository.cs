using CommonLibrary.Models;
using TaskManagement.Data.Entities;

namespace ConsumerService.Data.Repositories
{
    public interface ITaskRepository
    {
        public Task Create(TaskEntity task);
        public Task Update(TaskStatusEnum status, int updateId);
    }
}
