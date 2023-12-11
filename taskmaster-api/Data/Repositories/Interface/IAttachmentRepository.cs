using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface IAttachmentRepository
    {
        AttachmentEntity GetAttachmentById(int id);
        IEnumerable<AttachmentEntity> GetAllAttachments();
        AttachmentEntity CreateAttachment(AttachmentEntity attachment);
        AttachmentEntity UpdateAttachment(int id, AttachmentEntity attachment);
        int DeleteAttachment(int id);
    }
}
