using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class ProfileDto : IDto<ProfileEntity>
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AboutMe { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProfileDto()
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

        public ProfileEntity ToEntity()
        {
            return EntityHelpers.ToEntity<ProfileDto, ProfileEntity>(this);
        }
    }
}
