using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace taskmaster_api.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly ILogger<ActivityLogService> _logger;

        public ActivityLogService(IActivityLogRepository activityLogRepository, ILogger<ActivityLogService> logger)
        {
            _activityLogRepository = activityLogRepository;
            _logger = logger;
        }

        public ICoreActionResult<ActivityLogDto> GetActivityLogById(int id)
        {
            try
            {
                var activityLog = _activityLogRepository.GetActivityLogById(id);
                if (activityLog == null)
                {
                    _logger.LogInformation("ActivityLog not found");
                    return CoreActionResult<ActivityLogDto>.Failure("ActivityLog not found", "NotFound");
                }

                return CoreActionResult<ActivityLogDto>.Success(activityLog.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ActivityLogDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<ActivityLogDto>> GetAllActivityLogs()
        {
            try
            {
                var activityLogs = _activityLogRepository.GetAllActivityLogs();
                return CoreActionResult<List<ActivityLogDto>>.Success(activityLogs.Select(activityLog => activityLog.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<ActivityLogDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<ActivityLogDto> CreateActivityLog(ActivityLogDto activityLogDto)
        {
            try
            {
                var activityLog = activityLogDto.ToEntity();
                var newActivityLog = _activityLogRepository.CreateActivityLog(activityLog);
                return CoreActionResult<ActivityLogDto>.Success(newActivityLog.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ActivityLogDto>.Exception(ex);
            }
        }

        public ICoreActionResult<ActivityLogDto> UpdateActivityLog(int id, ActivityLogDto activityLogDto)
        {
            try
            {
                var activityLog = activityLogDto.ToEntity();
                var existingActivityLog = _activityLogRepository.GetActivityLogById(id);
                if (existingActivityLog == null)
                {
                    _logger.LogInformation("ActivityLog not found");
                    return CoreActionResult<ActivityLogDto>.Failure("ActivityLog not found", "NotFound");
                }

                var updatedActivityLog = _activityLogRepository.UpdateActivityLog(id, activityLog);
                return CoreActionResult<ActivityLogDto>.Success(updatedActivityLog.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ActivityLogDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteActivityLog(int id)
        {
            try
            {
                var deletedActivityLogId = _activityLogRepository.DeleteActivityLog(id);
                if (deletedActivityLogId == 0)
                {
                    _logger.LogInformation("ActivityLog not found");
                    return CoreActionResult.Ignore("ActivityLog not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }

        public ICoreActionResult LogActivity(string userId, string action)
        {
            try
            {
                var activityLog = new Data.Entities.ActivityLogEntity
                {
                    UserId = userId,
                    Action = action,
                    CreatedAt = DateTime.UtcNow
                };
                var newActivityLog = _activityLogRepository.CreateActivityLog(activityLog);
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }
    }
}
