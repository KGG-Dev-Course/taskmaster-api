using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ApplicationControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public IActionResult GetAllAttachments()
        {
            return ToHttpResult<List<AttachmentDto>>(_attachmentService.GetAllAttachments());
        }

        [HttpGet("{id}")]
        public IActionResult GetAttachment(int id)
        {
            return ToHttpResult<AttachmentDto>(_attachmentService.GetAttachmentById(id));
        }

        [HttpPost]
        public IActionResult CreateAttachment(AttachmentDto attachmentDto)
        {
            return ToHttpResult<AttachmentDto>(_attachmentService.CreateAttachment(attachmentDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttachment(int id, AttachmentDto attachmentDto)
        {
            return ToHttpResult<AttachmentDto>(_attachmentService.UpdateAttachment(id, attachmentDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttachment(int id)
        {
            return ToHttpResult(_attachmentService.DeleteAttachment(id));
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] AttachmentUploadRequest request)
        {
            return ToHttpResult(_attachmentService.UploadFile(request));
        }
    }
}
