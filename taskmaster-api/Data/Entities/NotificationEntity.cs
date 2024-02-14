using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Utilities;
using taskmaster_api.Data.Entities.Interface;

namespace taskmaster_api.Data.Entities
{
    public class NotificationEntity : IEntity<NotificationDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public string? UserId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public NotificationDto ToDto()
        {
            return EntityHelpers.ToDto<NotificationEntity, NotificationDto>(this);
        }
    }
}
