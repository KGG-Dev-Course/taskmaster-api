using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ICourseRepository
    {
        CourseEntity GetCourseById(int id);
        IEnumerable<CourseEntity> GetAllCourses();
        CourseEntity CreateCourse(CourseEntity ticket);
        CourseEntity UpdateCourse(int id, CourseEntity ticket);
        int DeleteCourse(int id);
    }
}
