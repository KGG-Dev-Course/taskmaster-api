using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public NotificationEntity GetNotificationById(int id)
        {
            return _context.Notifications.Find(id);
        }

        public IEnumerable<NotificationEntity> GetAllNotifications()
        {
            return _context.Notifications.ToList();
        }

        public NotificationEntity CreateNotification(NotificationEntity notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return notification;
        }

        public NotificationEntity UpdateNotification(int id, NotificationEntity notification)
        {
            if (_context.Notifications.Find(id) is NotificationEntity oldNotification)
            {
                notification.Id = id;
                _context.Notifications.Entry(oldNotification).State = EntityState.Detached;
                _context.Notifications.Entry(notification).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return notification;
        }

        public int DeleteNotification(int id)
        {
            var notificationToDelete = _context.Notifications.Find(id);
            if (notificationToDelete != null)
            {
                _context.Notifications.Remove(notificationToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
