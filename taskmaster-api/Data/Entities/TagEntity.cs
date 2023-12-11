using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Utilities;
using taskmaster_api.Data.Entities.Interface;

namespace taskmaster_api.Data.Entities
{
    public class TagEntity : IEntity<TagDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public TagDto ToDto()
        {
            return EntityHelpers.ToDto<TagEntity, TagDto>(this);
        }
    }
}
