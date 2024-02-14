using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class CourseDto : IDto<CourseEntity>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string InstructorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CourseDto()
        {
            Status = Models.CourseStatus.Inactive;
            if (Id.HasValue)
            {
                UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public CourseEntity ToEntity()
        {
            return EntityHelpers.ToEntity<CourseDto, CourseEntity>(this);
        }
    }
}
