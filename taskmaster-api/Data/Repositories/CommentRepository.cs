using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CommentEntity GetCommentById(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<CommentEntity> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public CommentEntity CreateComment(CommentEntity comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public CommentEntity UpdateComment(int id, CommentEntity comment)
        {
            if (_context.Comments.Find(id) is CommentEntity oldComment)
            {
                comment.Id = id;
                _context.Comments.Entry(oldComment).State = EntityState.Detached;
                _context.Comments.Entry(comment).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return comment;
        }

        public int DeleteComment(int id)
        {
            var commentToDelete = _context.Comments.Find(id);
            if (commentToDelete != null)
            {
                _context.Comments.Remove(commentToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
