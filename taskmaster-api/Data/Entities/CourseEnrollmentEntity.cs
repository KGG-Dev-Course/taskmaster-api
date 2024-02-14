using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Utilities;
using taskmaster_api.Data.Entities.Interface;

namespace taskmaster_api.Data.Entities
{
    public class CourseEnrollmentEntity : IEntity<CourseEnrollmentDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("CourseId")]
        public virtual CourseEntity Course { get; set; }
        public int? CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        public CourseEnrollmentDto ToDto()
        {
            return EntityHelpers.ToDto<CourseEnrollmentEntity, CourseEnrollmentDto>(this);
        }
    }
}
