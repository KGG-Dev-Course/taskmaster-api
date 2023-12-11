using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<TagService> _logger;

        public TagService(ITagRepository tagRepository, ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _logger = logger;
        }

        public ICoreActionResult<TagDto> GetTagById(int id)
        {
            try
            {
                var tag = _tagRepository.GetTagById(id);
                if (tag == null)
                {
                    _logger.LogInformation("Tag not found");
                    return CoreActionResult<TagDto>.Failure("Tag not found", "NotFound");
                }

                return CoreActionResult<TagDto>.Success(tag.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TagDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<TagDto>> GetAllTags()
        {
            try
            {
                var tags = _tagRepository.GetAllTags();
                return CoreActionResult<List<TagDto>>.Success(tags.Select(tag => tag.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<TagDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<TagDto> CreateTag(TagDto tagDto)
        {
            try
            {
                var tag = tagDto.ToEntity();
                var newTag = _tagRepository.CreateTag(tag);
                return CoreActionResult<TagDto>.Success(newTag.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TagDto>.Exception(ex);
            }
        }

        public ICoreActionResult<TagDto> UpdateTag(int id, TagDto tagDto)
        {
            try
            {
                var tag = tagDto.ToEntity();
                var existingTag = _tagRepository.GetTagById(id);
                if (existingTag == null)
                {
                    _logger.LogInformation("Tag not found");
                    return CoreActionResult<TagDto>.Failure("Tag not found", "NotFound");
                }

                var updatedTag = _tagRepository.UpdateTag(id, tag);
                return CoreActionResult<TagDto>.Success(updatedTag.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TagDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteTag(int id)
        {
            try
            {
                var deletedTagId = _tagRepository.DeleteTag(id);
                if (deletedTagId == 0)
                {
                    _logger.LogInformation("Tag not found");
                    return CoreActionResult.Ignore("Tag not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }
    }
}
