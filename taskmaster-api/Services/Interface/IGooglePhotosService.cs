
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;

namespace taskmaster_api.Services.Interface
{
    public interface IGooglePhotosService
    {
        ICoreActionResult<List<MediaItemDto>> GetPhotos(string accessToken);
        ICoreActionResult<GooglePhotoUploadResponse> UploadPhotos(string accessToken, GooglePhotoUploadRequest request);
    }
}
