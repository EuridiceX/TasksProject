namespace TaskManagement.Data
{
    using Microsoft.EntityFrameworkCore;
    using TaskManagement.Entities;

    namespace RabitMqProductAPI.Data
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
            public DbSet<TaskEntity> Tasks { get; set; }
        }
    }
}
