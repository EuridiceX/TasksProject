using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Entities;

namespace TaskManagement.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
        }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
    
}
