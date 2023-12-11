using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface IActivityLogRepository
    {
        ActivityLogEntity GetActivityLogById(int id);
        IEnumerable<ActivityLogEntity> GetAllActivityLogs();
        ActivityLogEntity CreateActivityLog(ActivityLogEntity activityLog);
        ActivityLogEntity UpdateActivityLog(int id, ActivityLogEntity activityLog);
        int DeleteActivityLog(int id);
    }
}
