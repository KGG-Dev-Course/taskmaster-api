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

        public virtual DbSet<ProfileEntity> Profiles { get; set; }
        public virtual DbSet<TicketEntity> Tickets { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }
        public virtual DbSet<AttachmentEntity> Attachments { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }
        public virtual DbSet<ActivityLogEntity> ActivityLogs { get; set; }
        public virtual DbSet<NotificationEntity> Notifications { get; set; }
        public virtual DbSet<SettingEntity> Settings { get; set; }
    }
}
