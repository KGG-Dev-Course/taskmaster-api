using Azure.Core;
using System.Net.Http.Headers;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class GooglePhotosService : IGooglePhotosService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://photoslibrary.googleapis.com/v1/";

        public GooglePhotosService() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<string> FetchPhotos(string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await _httpClient.GetAsync($"mediaItems");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Failed to fetch photos from Google Photos API");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception("Error fetching photos", ex);
            }
        }
    }
}
