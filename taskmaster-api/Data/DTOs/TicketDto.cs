using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class TicketDto : IDto<TicketEntity>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TicketDto()
        {
            Status = Models.TicketStatus.Pending;
            if (Id.HasValue)
            {
                UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public TicketEntity ToEntity()
        {
            return EntityHelpers.ToEntity<TicketDto, TicketEntity>(this);
        }
    }
}
