using System.ComponentModel.DataAnnotations;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Entities.Interface;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.Entities
{
    public class UserEntity: IEntity<UserDto>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        public UserDto ToDto()
        {
            return EntityHelpers.ToDto<UserEntity, UserDto>(this);
        }
    }
}
