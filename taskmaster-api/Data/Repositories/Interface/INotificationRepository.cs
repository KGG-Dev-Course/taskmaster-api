using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface INotificationRepository
    {
        NotificationEntity GetNotificationById(int id);
        IEnumerable<NotificationEntity> GetAllNotifications();
        NotificationEntity CreateNotification(NotificationEntity notification);
        NotificationEntity UpdateNotification(int id, NotificationEntity notification);
        int DeleteNotification(int id);
    }
}
