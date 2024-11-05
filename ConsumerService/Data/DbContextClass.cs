using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.Data.Entities;

namespace ConsumerService.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(DbContextOptions<DbContextClass> options)
       : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
    
}
