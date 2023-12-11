using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface ICommentService
    {
        ICoreActionResult<CommentDto> GetCommentById(int id);
        ICoreActionResult<List<CommentDto>> GetAllComments();
        ICoreActionResult<CommentDto> CreateComment(CommentDto commentDto);
        ICoreActionResult<CommentDto> UpdateComment(int id, CommentDto commentDto);
        ICoreActionResult DeleteComment(int id);
    }
}
