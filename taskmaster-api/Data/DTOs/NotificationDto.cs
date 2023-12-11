using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class NotificationDto : IDto<NotificationEntity>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public NotificationDto()
        {
            if (!Id.HasValue)
            {
                IsRead = false;
                CreatedAt = DateTime.UtcNow;
            }
        }

        public NotificationEntity ToEntity()
        {
            return EntityHelpers.ToEntity<NotificationDto, NotificationEntity>(this);
        }
    }
}
