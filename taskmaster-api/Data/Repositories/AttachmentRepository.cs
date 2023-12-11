using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AttachmentEntity GetAttachmentById(int id)
        {
            return _context.Attachments.Find(id);
        }

        public IEnumerable<AttachmentEntity> GetAllAttachments()
        {
            return _context.Attachments.ToList();
        }

        public AttachmentEntity CreateAttachment(AttachmentEntity attachment)
        {
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
            return attachment;
        }

        public AttachmentEntity UpdateAttachment(int id, AttachmentEntity attachment)
        {
            if (_context.Attachments.Find(id) is AttachmentEntity oldAttachment)
            {
                attachment.Id = id;
                _context.Attachments.Entry(oldAttachment).State = EntityState.Detached;
                _context.Attachments.Entry(attachment).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return attachment;
        }

        public int DeleteAttachment(int id)
        {
            var attachmentToDelete = _context.Attachments.Find(id);
            if (attachmentToDelete != null)
            {
                _context.Attachments.Remove(attachmentToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
