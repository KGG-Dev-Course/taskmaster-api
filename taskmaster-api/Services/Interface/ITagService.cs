using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface ITagService
    {
        ICoreActionResult<TagDto> GetTagById(int id);
        ICoreActionResult<List<TagDto>> GetAllTags();
        ICoreActionResult<TagDto> CreateTag(TagDto tagDto);
        ICoreActionResult<TagDto> UpdateTag(int id, TagDto tagDto);
        ICoreActionResult DeleteTag(int id);
    }
}
