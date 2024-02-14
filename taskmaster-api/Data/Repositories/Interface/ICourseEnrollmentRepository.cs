using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ICourseEnrollmentRepository
    {
        CourseEnrollmentEntity GetCourseEnrollmentById(int id);
        IEnumerable<CourseEnrollmentEntity> GetAllCourseEnrollments();
        CourseEnrollmentEntity CreateCourseEnrollment(CourseEnrollmentEntity ticket);
        CourseEnrollmentEntity UpdateCourseEnrollment(int id, CourseEnrollmentEntity ticket);
        int DeleteCourseEnrollment(int id);
    }
}
