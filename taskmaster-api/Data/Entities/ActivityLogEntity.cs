using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Utilities;
using taskmaster_api.Data.Entities.Interface;

namespace taskmaster_api.Data.Entities
{
    public class ActivityLogEntity : IEntity<ActivityLogDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ActivityLogDto ToDto()
        {
            return EntityHelpers.ToDto<ActivityLogEntity, ActivityLogDto>(this);
        }
    }
}
