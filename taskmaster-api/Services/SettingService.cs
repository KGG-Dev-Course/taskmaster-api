using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ILogger<SettingService> _logger;

        public SettingService(ISettingRepository settingRepository, ILogger<SettingService> logger)
        {
            _settingRepository = settingRepository;
            _logger = logger;
        }

        public ICoreActionResult<SettingDto> GetSettingById(int id)
        {
            try
            {
                var setting = _settingRepository.GetSettingById(id);
                if (setting == null)
                {
                    _logger.LogInformation("Setting not found");
                    return CoreActionResult<SettingDto>.Failure("Setting not found", "NotFound");
                }

                return CoreActionResult<SettingDto>.Success(setting.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<SettingDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<SettingDto>> GetAllSettings()
        {
            try
            {
                var settings = _settingRepository.GetAllSettings();
                return CoreActionResult<List<SettingDto>>.Success(settings.Select(setting => setting.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<SettingDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<SettingDto> CreateSetting(SettingDto settingDto)
        {
            try
            {
                var setting = settingDto.ToEntity();
                var newSetting = _settingRepository.CreateSetting(setting);
                return CoreActionResult<SettingDto>.Success(newSetting.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<SettingDto>.Exception(ex);
            }
        }

        public ICoreActionResult<SettingDto> UpdateSetting(int id, SettingDto settingDto)
        {
            try
            {
                var setting = settingDto.ToEntity();
                var existingSetting = _settingRepository.GetSettingById(id);
                if (existingSetting == null)
                {
                    _logger.LogInformation("Setting not found");
                    return CoreActionResult<SettingDto>.Failure("Setting not found", "NotFound");
                }

                var updatedSetting = _settingRepository.UpdateSetting(id, setting);
                return CoreActionResult<SettingDto>.Success(updatedSetting.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<SettingDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteSetting(int id)
        {
            try
            {
                var deletedSettingId = _settingRepository.DeleteSetting(id);
                if (deletedSettingId == 0)
                {
                    _logger.LogInformation("Setting not found");
                    return CoreActionResult.Ignore("Setting not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }
    }
}
