using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;

namespace taskmaster_api.Services.Interface
{
    public interface IProfileService
    {
        ICoreActionResult<ProfileDto> GetProfileById(int id);
        ICoreActionResult<List<ProfileDto>> GetAllProfiles();
        ICoreActionResult<ProfileDto> CreateProfile(ProfileDto profileDto);
        ICoreActionResult<ProfileDto> UpdateProfile(int id, ProfileDto profileDto);
        ICoreActionResult DeleteProfile(int id);
        ICoreActionResult<ProfileDto> GetProfileByUserId(string userId);
        ICoreActionResult<ProfileUploadResponse> UploadPhoto(ProfileUploadRequest request);
        byte[] GetPhoto(string fileName);
    }
}
