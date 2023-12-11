using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ICommentRepository
    {
        CommentEntity GetCommentById(int id);
        IEnumerable<CommentEntity> GetAllComments();
        CommentEntity CreateComment(CommentEntity comment);
        CommentEntity UpdateComment(int id, CommentEntity comment);
        int DeleteComment(int id);
    }
}
