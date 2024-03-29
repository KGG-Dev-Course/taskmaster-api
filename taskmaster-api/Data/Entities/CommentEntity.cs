﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Entities.Interface;
using taskmaster_api.Utilities;
using Microsoft.AspNetCore.Identity;

namespace taskmaster_api.Data.Entities
{
    public class CommentEntity : IEntity<CommentDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("TicketId")]
        public virtual TicketEntity Ticket { get; set; }
        public int? TicketId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public CommentDto ToDto()
        {
            return EntityHelpers.ToDto<CommentEntity, CommentDto>(this);
        }
    }
}
