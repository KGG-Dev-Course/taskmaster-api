using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Entities.Interface;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.Entities
{
    public class CourseEntity : IEntity<CourseDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Duration { get; set; }

        [ForeignKey("InstructorId")]
        public virtual IdentityUser Instructor { get; set; }
        public string? InstructorId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public CourseDto ToDto()
        {
            return EntityHelpers.ToDto<CourseEntity, CourseDto>(this);
        }
    }
}
