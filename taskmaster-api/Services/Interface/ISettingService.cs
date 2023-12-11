using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface ISettingService
    {
        ICoreActionResult<SettingDto> GetSettingById(int id);
        ICoreActionResult<List<SettingDto>> GetAllSettings();
        ICoreActionResult<SettingDto> CreateSetting(SettingDto settingDto);
        ICoreActionResult<SettingDto> UpdateSetting(int id, SettingDto settingDto);
        ICoreActionResult DeleteSetting(int id);
    }
}
