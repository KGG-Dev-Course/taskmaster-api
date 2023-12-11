using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface IAttachmentService
    {
        ICoreActionResult<AttachmentDto> GetAttachmentById(int id);
        ICoreActionResult<List<AttachmentDto>> GetAllAttachments();
        ICoreActionResult<AttachmentDto> CreateAttachment(AttachmentDto attachmentDto);
        ICoreActionResult<AttachmentDto> UpdateAttachment(int id, AttachmentDto attachmentDto);
        ICoreActionResult DeleteAttachment(int id);
        ICoreActionResult<AttachmentUploadResult> UploadFile(AttachmentUploadRequest request);
    }
}
