using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class CourseEnrollmentRepository : ICourseEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CourseEnrollmentEntity GetCourseEnrollmentById(int id)
        {
            return _context.CourseEnrollments.Find(id);
        }

        public IEnumerable<CourseEnrollmentEntity> GetAllCourseEnrollments()
        {
            return _context.CourseEnrollments.ToList();
        }

        public CourseEnrollmentEntity CreateCourseEnrollment(CourseEnrollmentEntity comment)
        {
            _context.CourseEnrollments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public CourseEnrollmentEntity UpdateCourseEnrollment(int id, CourseEnrollmentEntity comment)
        {
            if (_context.CourseEnrollments.Find(id) is CourseEnrollmentEntity oldCourseEnrollment)
            {
                comment.Id = id;
                _context.CourseEnrollments.Entry(oldCourseEnrollment).State = EntityState.Detached;
                _context.CourseEnrollments.Entry(comment).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return comment;
        }

        public int DeleteCourseEnrollment(int id)
        {
            var commentToDelete = _context.CourseEnrollments.Find(id);
            if (commentToDelete != null)
            {
                _context.CourseEnrollments.Remove(commentToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
