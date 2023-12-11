using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public ICoreActionResult<CommentDto> GetCommentById(int id)
        {
            try
            {
                var comment = _commentRepository.GetCommentById(id);
                if (comment == null)
                {
                    _logger.LogInformation("Comment not found");
                    return CoreActionResult<CommentDto>.Failure("Comment not found", "NotFound");
                }

                return CoreActionResult<CommentDto>.Success(comment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<CommentDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<CommentDto>> GetAllComments()
        {
            try
            {
                var comments = _commentRepository.GetAllComments();
                return CoreActionResult<List<CommentDto>>.Success(comments.Select(comment => comment.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<CommentDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<CommentDto> CreateComment(CommentDto commentDto)
        {
            try
            {
                var comment = commentDto.ToEntity();
                var newComment = _commentRepository.CreateComment(comment);
                return CoreActionResult<CommentDto>.Success(newComment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<CommentDto>.Exception(ex);
            }
        }

        public ICoreActionResult<CommentDto> UpdateComment(int id, CommentDto commentDto)
        {
            try
            {
                var comment = commentDto.ToEntity();
                var existingComment = _commentRepository.GetCommentById(id);
                if (existingComment == null)
                {
                    _logger.LogInformation("Comment not found");
                    return CoreActionResult<CommentDto>.Failure("Comment not found", "NotFound");
                }

                var updatedComment = _commentRepository.UpdateComment(id, comment);
                return CoreActionResult<CommentDto>.Success(updatedComment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<CommentDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteComment(int id)
        {
            try
            {
                var deletedCommentId = _commentRepository.DeleteComment(id);
                if (deletedCommentId == 0)
                {
                    _logger.LogInformation("Comment not found");
                    return CoreActionResult.Ignore("Comment not found", "NotFound");
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
