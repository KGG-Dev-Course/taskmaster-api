using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Utilities;

namespace taskmaster_api.Data.DTOs
{
    public class TagDto : IDto<TagEntity>
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public TagDto()
        {
        }

        public TagEntity ToEntity()
        {
            return EntityHelpers.ToEntity<TagDto, TagEntity>(this);
        }
    }
}
