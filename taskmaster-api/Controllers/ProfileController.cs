using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ApplicationControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult GetAllProfiles()
        {
            return ToHttpResult<List<ProfileDto>>(_profileService.GetAllProfiles());
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            return ToHttpResult<ProfileDto>(_profileService.GetProfileById(id));
        }

        [HttpPost]
        public IActionResult CreateProfile(ProfileDto profileDto)
        {
            return ToHttpResult<ProfileDto>(_profileService.CreateProfile(profileDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfile(int id, ProfileDto profileDto)
        {
            return ToHttpResult<ProfileDto>(_profileService.UpdateProfile(id, profileDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(int id)
        {
            return ToHttpResult(_profileService.DeleteProfile(id));
        }

        [HttpGet("mine")]
        public IActionResult GetMineProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return ToHttpResult<ProfileDto>(_profileService.GetProfileByUserId(userId));
        }

        [HttpPost("uploadPhoto")]
        public IActionResult UploadPhoto([FromForm] ProfileUploadRequest request)
        {
            return ToHttpResult<ProfileUploadResponse>(_profileService.UploadPhoto(request));
        }

        [HttpGet("getPhoto/{fileName}")]
        [AllowAnonymous]
        public IActionResult GetPhoto(string fileName)
        {
            var imageBytes = _profileService.GetPhoto(fileName);

            if (imageBytes != null)
            {
                return File(imageBytes, "image/jpeg");
            }

            return NotFound("Image not found");
        }
    }
}
