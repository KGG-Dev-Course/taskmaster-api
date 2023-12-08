using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<TaskEntity> Tasks { get; set; }
    }
}
