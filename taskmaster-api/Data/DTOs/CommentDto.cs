using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class CommentDto : IDto<CommentEntity>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CommentDto()
        {
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

        public CommentEntity ToEntity()
        {
            return EntityHelpers.ToEntity<CommentDto, CommentEntity>(this);
        }
    }
}
