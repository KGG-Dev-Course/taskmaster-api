using Azure.Core;
using System.Net.Http.Headers;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;
using System.Text;
using Azure;

namespace taskmaster_api.Services
{
    public class GooglePhotosService : IGooglePhotosService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationService> _logger;
        private const string BaseUrl = "https://photoslibrary.googleapis.com/v1/";

        public GooglePhotosService(ILogger<NotificationService> logger) {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _logger = logger;
        }

        public ICoreActionResult<List<MediaItemDto>> GetPhotos(string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = _httpClient.GetAsync("mediaItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MediaResult>(json);

                    // Map to MediaItemDto
                    List<MediaItemDto> mediaList = result.mediaItems.ConvertAll(x => new MediaItemDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        MimeType = x.MimeType,
                        Filename = x.Filename,
                        BaseUrl = x.BaseUrl,
                        ProductUrl = x.ProductUrl
                    });

                    return CoreActionResult<List<MediaItemDto>>.Success(mediaList);
                }
                else
                {
                    _logger.LogInformation("Failed to fetch photos from Google Photos API");
                    return CoreActionResult<List<MediaItemDto>>.Failure("Failed to fetch photos from Google Photos API", "NotFound");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<MediaItemDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<GooglePhotoUploadResponse> UploadPhotos(string accessToken, GooglePhotoUploadRequest request)
        {
            if (request == null || request.Files == null || request.Files.Count <= 0)
            {
                return CoreActionResult<GooglePhotoUploadResponse>.Failure("Invalid file");
            }

            try
            {
                // Create a list to hold all the image data
                List<byte[]> imageDataList = new List<byte[]>();

                // Process each file in the request
                foreach (var file in request.Files)
                {
                    // Check if the file is not null and has content
                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            // Read the file into a memory stream
                            file.CopyTo(memoryStream);

                            // Store the image data as a byte array
                            imageDataList.Add(memoryStream.ToArray());
                        }
                    }
                }

                // Check if any valid image data was extracted
                if (imageDataList.Count == 0)
                {
                    return CoreActionResult<GooglePhotoUploadResponse>.Failure("No valid image data found", "NotFound");
                }

                GooglePhotoUploadResponse result = new GooglePhotoUploadResponse();
                List<string> uploadedImageUrls = new List<string>();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var uploadTokenList = new List<string>();

                foreach (var imageData in imageDataList)
                {
                    string base64Image = Convert.ToBase64String(imageData);

                    // Construct the request for obtaining an upload token
                    var content = new StringContent(base64Image, Encoding.UTF8, "application/octet-stream");
                    var response = _httpClient.PostAsync("https://photoslibrary.googleapis.com/v1/uploads", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        uploadTokenList.Add(responseContent.Trim()); // Store the obtained upload token
                    }
                    else
                    {
                        // Handle upload failure
                        return CoreActionResult<GooglePhotoUploadResponse>.Failure("Failed to obtain upload token", "UploadError");
                    }
                }

                // Step 2: Create media items using obtained upload tokens
                var createMediaItemsPayload = new
                {
                    //albumId = "YOUR_ALBUM_ID", // Replace with your album ID
                    newMediaItems = uploadTokenList.Select(token => new
                    {
                        description = "Image Description", // Add description if needed
                        simpleMediaItem = new
                        {
                            uploadToken = token // Use the obtained upload token
                        }
                    })
                };

                // Serialize the payload for creating media items
                string createMediaItemsJson = Newtonsoft.Json.JsonConvert.SerializeObject(createMediaItemsPayload);
                var createMediaItemsContent = new StringContent(createMediaItemsJson, Encoding.UTF8, "application/json");

                // Send the POST request to create media items
                var createMediaItemsResponse = _httpClient.PostAsync("https://photoslibrary.googleapis.com/v1/mediaItems:batchCreate", createMediaItemsContent).Result;

                if (createMediaItemsResponse.IsSuccessStatusCode)
                {
                    var responseContent = createMediaItemsResponse.Content.ReadAsStringAsync().Result;
                    // Handle successful creation of media items
                    return CoreActionResult<GooglePhotoUploadResponse>.Success(new GooglePhotoUploadResponse { /* Populate with relevant data */ });
                }
                else
                {
                    // Handle failure in creating media items
                    return CoreActionResult<GooglePhotoUploadResponse>.Failure("Failed to create media items", "CreateMediaItemsError");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<GooglePhotoUploadResponse>.Exception(ex);
            }
        }
    }

    public class MediaResult
    {
        public List<MediaItemDto> mediaItems { get; set; }
    }
}
