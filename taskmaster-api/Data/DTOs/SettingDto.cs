using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class SettingDto : IDto<SettingEntity>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Description { get; set; }

        public SettingDto()
        {
        }

        public SettingEntity ToEntity()
        {
            return EntityHelpers.ToEntity<SettingDto, SettingEntity>(this);
        }
    }
}
