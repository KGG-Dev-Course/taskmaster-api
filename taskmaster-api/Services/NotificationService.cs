using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository, ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public ICoreActionResult<NotificationDto> GetNotificationById(int id)
        {
            try
            {
                var notification = _notificationRepository.GetNotificationById(id);
                if (notification == null)
                {
                    _logger.LogInformation("Notification not found");
                    return CoreActionResult<NotificationDto>.Failure("Notification not found", "NotFound");
                }

                return CoreActionResult<NotificationDto>.Success(notification.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<NotificationDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<NotificationDto>> GetAllNotifications()
        {
            try
            {
                var notifications = _notificationRepository.GetAllNotifications();
                return CoreActionResult<List<NotificationDto>>.Success(notifications.Select(notification => notification.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<NotificationDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<NotificationDto> CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                var notification = notificationDto.ToEntity();
                var newNotification = _notificationRepository.CreateNotification(notification);
                return CoreActionResult<NotificationDto>.Success(newNotification.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<NotificationDto>.Exception(ex);
            }
        }

        public ICoreActionResult<NotificationDto> UpdateNotification(int id, NotificationDto notificationDto)
        {
            try
            {
                var notification = notificationDto.ToEntity();
                var existingNotification = _notificationRepository.GetNotificationById(id);
                if (existingNotification == null)
                {
                    _logger.LogInformation("Notification not found");
                    return CoreActionResult<NotificationDto>.Failure("Notification not found", "NotFound");
                }

                var updatedNotification = _notificationRepository.UpdateNotification(id, notification);
                return CoreActionResult<NotificationDto>.Success(updatedNotification.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<NotificationDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteNotification(int id)
        {
            try
            {
                var deletedNotificationId = _notificationRepository.DeleteNotification(id);
                if (deletedNotificationId == 0)
                {
                    _logger.LogInformation("Notification not found");
                    return CoreActionResult.Ignore("Notification not found", "NotFound");
                }
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
