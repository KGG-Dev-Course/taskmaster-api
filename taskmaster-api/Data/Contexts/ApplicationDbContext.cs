using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<TaskEntity> Tasks { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }
    }
}
