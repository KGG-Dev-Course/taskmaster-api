using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GooglePhotosController : ControllerBase
    {
        private readonly IGooglePhotosService _googlePhotosService;

        public GooglePhotosController(IGooglePhotosService googlePhotosService)
        {
            _googlePhotosService = googlePhotosService;
        }

        [HttpGet("fetch-photos")]
        public async Task<IActionResult> FetchPhotos(string accessToken)
        {
            try
            {
                var photos = await _googlePhotosService.FetchPhotos(accessToken);
                return Ok(photos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to fetch photos: {ex.Message}");
            }
        }
    }
}
