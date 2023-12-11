using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ApplicationControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            return ToHttpResult<List<CommentDto>>(_commentService.GetAllComments());
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            return ToHttpResult<CommentDto>(_commentService.GetCommentById(id));
        }

        [HttpPost]
        public IActionResult CreateComment(CommentDto commentDto)
        {
            return ToHttpResult<CommentDto>(_commentService.CreateComment(commentDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, CommentDto commentDto)
        {
            return ToHttpResult<CommentDto>(_commentService.UpdateComment(id, commentDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            return ToHttpResult(_commentService.DeleteComment(id));
        }
    }
}
