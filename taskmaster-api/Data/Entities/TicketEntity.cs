using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Entities.Interface;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.Entities
{
    public class TicketEntity : IEntity<TicketDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public TicketDto ToDto()
        {
            return EntityHelpers.ToDto<TicketEntity, TicketDto>(this);
        }
    }
}
