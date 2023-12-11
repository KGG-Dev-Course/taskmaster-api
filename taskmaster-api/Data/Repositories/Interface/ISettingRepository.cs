using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ISettingRepository
    {
        SettingEntity GetSettingById(int id);
        IEnumerable<SettingEntity> GetAllSettings();
        SettingEntity CreateSetting(SettingEntity setting);
        SettingEntity UpdateSetting(int id, SettingEntity setting);
        int DeleteSetting(int id);
    }
}
