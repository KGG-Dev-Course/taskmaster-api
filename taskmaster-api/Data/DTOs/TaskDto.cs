using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class TaskDto : IDto<TaskEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        public TaskEntity ToEntity()
        {
            return DtoHelpers.ToEntity<TaskDto, TaskEntity>(this);
        }
    }
}
