using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ProfileService> _logger;
        private readonly string UploadFolderPath = "Uploads/profile";

        public ProfileService(IProfileRepository profileRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ILogger<ProfileService> logger)
        {
            _profileRepository = profileRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public ICoreActionResult<ProfileDto> GetProfileById(int id)
        {
            try
            {
                var profile = _profileRepository.GetProfileById(id);
                if (profile == null)
                {
                    _logger.LogInformation("Profile not found");
                    return CoreActionResult<ProfileDto>.Failure("Profile not found", "NotFound");
                }

                return CoreActionResult<ProfileDto>.Success(profile.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ProfileDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<ProfileDto>> GetAllProfiles()
        {
            try
            {
                var profiles = _profileRepository.GetAllProfiles();
                return CoreActionResult<List<ProfileDto>>.Success(profiles.Select(profile => profile.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<ProfileDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<ProfileDto> CreateProfile(ProfileDto profileDto)
        {
            try
            {
                var profile = profileDto.ToEntity();
                var newProfile = _profileRepository.CreateProfile(profile);
                return CoreActionResult<ProfileDto>.Success(newProfile.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ProfileDto>.Exception(ex);
            }
        }

        public ICoreActionResult<ProfileDto> UpdateProfile(int id, ProfileDto profileDto)
        {
            try
            {
                var profile = profileDto.ToEntity();
                var existingProfile = _profileRepository.GetProfileById(id);
                if (existingProfile == null)
                {
                    _logger.LogInformation("Profile not found");
                    return CoreActionResult<ProfileDto>.Failure("Profile not found", "NotFound");
                }

                if (profile.Photo != existingProfile.Photo && existingProfile.Photo != null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, UploadFolderPath, existingProfile.Photo);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        _logger.LogInformation("File does not exist.");
                    }
                }

                var updatedProfile = _profileRepository.UpdateProfile(id, profile);
                return CoreActionResult<ProfileDto>.Success(updatedProfile.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ProfileDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteProfile(int id)
        {
            try
            {
                var deletedProfileId = _profileRepository.DeleteProfile(id);
                if (deletedProfileId == 0)
                {
                    _logger.LogInformation("Profile not found");
                    return CoreActionResult.Ignore("Profile not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }

        public ICoreActionResult<ProfileDto> GetProfileByUserId(string userId)
        {
            try
            {
                var profile = _profileRepository.GetProfileByUserId(userId);
                if (profile == null)
                {
                    _logger.LogInformation("Profile not found");
                    return CoreActionResult<ProfileDto>.Failure("Profile not found", "NotFound");
                }

                return CoreActionResult<ProfileDto>.Success(profile.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ProfileDto>.Exception(ex);
            }
        }

        public ICoreActionResult<ProfileUploadResponse> UploadPhoto(ProfileUploadRequest request)
        {
            if (request == null || request.File == null || request.File.Length <= 0)
            {
                return CoreActionResult<ProfileUploadResponse>.Failure("Invalid file");
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

                return CoreActionResult<ProfileUploadResponse>.Success(new ProfileUploadResponse { Success = true, FilePath = filePath, FileName = fileName });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<ProfileUploadResponse>.Exception(ex);
            }
        }

        public byte[] GetPhoto(string fileName)
        {
            try
            {
                var imagePath = Path.Combine(UploadFolderPath, fileName);

                if (File.Exists(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    return imageBytes;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}
