
namespace taskmaster_api.Services.Interface
{
    public interface IGooglePhotosService
    {
        Task<string> FetchPhotos(string accessToken);
    }
}
