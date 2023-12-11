using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface IActivityLogService
    {
        ICoreActionResult<ActivityLogDto> GetActivityLogById(int id);
        ICoreActionResult<List<ActivityLogDto>> GetAllActivityLogs();
        ICoreActionResult<ActivityLogDto> CreateActivityLog(ActivityLogDto activityLogDto);
        ICoreActionResult<ActivityLogDto> UpdateActivityLog(int id, ActivityLogDto activityLogDto);
        ICoreActionResult DeleteActivityLog(int id);
        ICoreActionResult LogActivity(string userId,  string action);
    }
}
