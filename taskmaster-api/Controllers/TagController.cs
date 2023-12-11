using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ApplicationControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            return ToHttpResult<List<TagDto>>(_tagService.GetAllTags());
        }

        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            return ToHttpResult<TagDto>(_tagService.GetTagById(id));
        }

        [HttpPost]
        public IActionResult CreateTag(TagDto tagDto)
        {
            return ToHttpResult<TagDto>(_tagService.CreateTag(tagDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, TagDto tagDto)
        {
            return ToHttpResult<TagDto>(_tagService.UpdateTag(id, tagDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            return ToHttpResult(_tagService.DeleteTag(id));
        }
    }
}
