using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TodoItems { get; set; }
    }
}