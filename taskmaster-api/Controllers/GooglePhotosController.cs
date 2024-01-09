using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GooglePhotosController : ApplicationControllerBase
    {
        private readonly IGooglePhotosService _googlePhotosService;

        public GooglePhotosController(IGooglePhotosService googlePhotosService)
        {
            _googlePhotosService = googlePhotosService;
        }

        [HttpGet("getPhotos")]
        public IActionResult GetPhotos(string accessToken)
        {
            return ToHttpResult<List<MediaItemDto>>(_googlePhotosService.GetPhotos(accessToken));
        }

        [HttpPost("uploadPhoto")]
        public IActionResult UploadPhoto([FromForm(Name = "accessToken")] string accessToken, [FromForm] GooglePhotoUploadRequest request)
        {
            return ToHttpResult<GooglePhotoUploadResponse>(_googlePhotosService.UploadPhotos(accessToken, request));
        }
    }
}
