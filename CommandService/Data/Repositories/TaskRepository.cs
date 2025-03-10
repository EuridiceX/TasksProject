﻿using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Entities;

namespace ConsumerService.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContextClass _dbContext;
        public TaskRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TaskEntity task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TaskStatusEnum status, int updateId)
        {
            var updateEntity = await _dbContext.Tasks.FirstAsync(x => x.Id == updateId);

            updateEntity.Status = (int)status;

             _dbContext.Tasks.Update(updateEntity);

            await _dbContext.SaveChangesAsync();

        }
    }
}
