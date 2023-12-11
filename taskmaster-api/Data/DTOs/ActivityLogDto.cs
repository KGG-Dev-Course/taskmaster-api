using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class ActivityLogDto : IDto<ActivityLogEntity>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }

        public ActivityLogDto()
        {
            if (!Id.HasValue)
            {
                CreatedAt = DateTime.UtcNow;
            }
        }

        public ActivityLogEntity ToEntity()
        {
            return EntityHelpers.ToEntity<ActivityLogDto, ActivityLogEntity>(this);
        }
    }
}
