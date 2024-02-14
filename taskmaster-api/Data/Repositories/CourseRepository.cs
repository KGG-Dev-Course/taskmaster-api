using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CourseEntity GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public IEnumerable<CourseEntity> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public CourseEntity CreateCourse(CourseEntity ticket)
        {
            _context.Courses.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public CourseEntity UpdateCourse(int id, CourseEntity ticket)
        {
            if (_context.Courses.Find(id) is CourseEntity oldCourse)
            {
                ticket.Id = id;
                _context.Courses.Entry(oldCourse).State = EntityState.Detached;
                _context.Courses.Entry(ticket).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return ticket;
        }

        public int DeleteCourse(int id)
        {
            var ticketToDelete = _context.Courses.Find(id);
            if (ticketToDelete != null)
            {
                _context.Courses.Remove(ticketToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
