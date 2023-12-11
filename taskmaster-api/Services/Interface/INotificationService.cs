using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface INotificationService
    {
        ICoreActionResult<NotificationDto> GetNotificationById(int id);
        ICoreActionResult<List<NotificationDto>> GetAllNotifications();
        ICoreActionResult<NotificationDto> CreateNotification(NotificationDto notificationDto);
        ICoreActionResult<NotificationDto> UpdateNotification(int id, NotificationDto notificationDto);
        ICoreActionResult DeleteNotification(int id);
    }
}
