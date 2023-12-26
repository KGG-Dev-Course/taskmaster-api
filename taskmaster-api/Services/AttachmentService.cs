using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AttachmentService> _logger;
        private readonly string UploadFolderPath = "Uploads/attachment";

        public AttachmentService(IAttachmentRepository attachmentRepository, IWebHostEnvironment webHostEnvironment, ILogger<AttachmentService> logger)
        {
            _attachmentRepository = attachmentRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public ICoreActionResult<AttachmentDto> GetAttachmentById(int id)
        {
            try
            {
                var attachment = _attachmentRepository.GetAttachmentById(id);
                if (attachment == null)
                {
                    _logger.LogInformation("Attachment not found");
                    return CoreActionResult<AttachmentDto>.Failure("Attachment not found", "NotFound");
                }

                return CoreActionResult<AttachmentDto>.Success(attachment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<AttachmentDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<AttachmentDto>> GetAllAttachments()
        {
            try
            {
                var attachments = _attachmentRepository.GetAllAttachments();
                return CoreActionResult<List<AttachmentDto>>.Success(attachments.Select(attachment => attachment.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<AttachmentDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<AttachmentDto> CreateAttachment(AttachmentDto attachmentDto)
        {
            try
            {
                var attachment = attachmentDto.ToEntity();
                var newAttachment = _attachmentRepository.CreateAttachment(attachment);
                return CoreActionResult<AttachmentDto>.Success(newAttachment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<AttachmentDto>.Exception(ex);
            }
        }

        public ICoreActionResult<AttachmentDto> UpdateAttachment(int id, AttachmentDto attachmentDto)
        {
            try
            {
                var attachment = attachmentDto.ToEntity();
                var existingAttachment = _attachmentRepository.GetAttachmentById(id);
                if (existingAttachment == null)
                {
                    _logger.LogInformation("Attachment not found");
                    return CoreActionResult<AttachmentDto>.Failure("Attachment not found", "NotFound");
                }

                var updatedAttachment = _attachmentRepository.UpdateAttachment(id, attachment);
                return CoreActionResult<AttachmentDto>.Success(updatedAttachment.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<AttachmentDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteAttachment(int id)
        {
            try
            {
                var deletedAttachmentId = _attachmentRepository.DeleteAttachment(id);
                if (deletedAttachmentId == 0)
                {
                    _logger.LogInformation("Attachment not found");
                    return CoreActionResult.Ignore("Attachment not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }

        public ICoreActionResult<AttachmentUploadResult> UploadFile(AttachmentUploadRequest request)
        {
            if (request == null || request.File == null || request.File.Length <= 0)
            {
                return CoreActionResult<AttachmentUploadResult>.Failure("Invalid file");
            }

            try
            {
                // Create a unique filename to avoid overwriting existing files
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);

                // Combine the unique filename with the storage path
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, UploadFolderPath, fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(stream);
                }

                return CoreActionResult<AttachmentUploadResult>.Success(new AttachmentUploadResult { Success = true, FilePath = filePath, FileName = fileName });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<AttachmentUploadResult>.Exception(ex);
            }
        }
    }
}
