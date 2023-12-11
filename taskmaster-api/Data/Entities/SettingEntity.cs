using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Utilities;
using taskmaster_api.Data.Entities.Interface;

namespace taskmaster_api.Data.Entities
{
    public class SettingEntity : IEntity<SettingDto>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public string? Description { get; set; }

        public SettingDto ToDto()
        {
            return EntityHelpers.ToDto<SettingEntity, SettingDto>(this);
        }
    }
}
