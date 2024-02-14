using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class CourseEnrollmentDto : IDto<CourseEnrollmentEntity>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public CourseEnrollmentDto()
        {
            EnrollmentDate = DateTime.UtcNow;
        }

        public CourseEnrollmentEntity ToEntity()
        {
            return EntityHelpers.ToEntity<CourseEnrollmentDto, CourseEnrollmentEntity>(this);
        }
    }
}
