using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActivityLogEntity GetActivityLogById(int id)
        {
            return _context.ActivityLogs.Find(id);
        }

        public IEnumerable<ActivityLogEntity> GetAllActivityLogs()
        {
            return _context.ActivityLogs.ToList();
        }

        public ActivityLogEntity CreateActivityLog(ActivityLogEntity activityLog)
        {
            _context.ActivityLogs.Add(activityLog);
            _context.SaveChanges();
            return activityLog;
        }

        public ActivityLogEntity UpdateActivityLog(int id, ActivityLogEntity activityLog)
        {
            if (_context.ActivityLogs.Find(id) is ActivityLogEntity oldActivityLog)
            {
                activityLog.Id = id;
                _context.ActivityLogs.Entry(oldActivityLog).State = EntityState.Detached;
                _context.ActivityLogs.Entry(activityLog).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return activityLog;
        }

        public int DeleteActivityLog(int id)
        {
            var activityLogToDelete = _context.ActivityLogs.Find(id);
            if (activityLogToDelete != null)
            {
                _context.ActivityLogs.Remove(activityLogToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
