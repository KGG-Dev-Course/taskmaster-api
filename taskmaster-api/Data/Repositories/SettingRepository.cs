using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationDbContext _context;

        public SettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public SettingEntity GetSettingById(int id)
        {
            return _context.Settings.Find(id);
        }

        public IEnumerable<SettingEntity> GetAllSettings()
        {
            return _context.Settings.ToList();
        }

        public SettingEntity CreateSetting(SettingEntity setting)
        {
            _context.Settings.Add(setting);
            _context.SaveChanges();
            return setting;
        }

        public SettingEntity UpdateSetting(int id, SettingEntity setting)
        {
            if (_context.Settings.Find(id) is SettingEntity oldSetting)
            {
                setting.Id = id;
                _context.Settings.Entry(oldSetting).State = EntityState.Detached;
                _context.Settings.Entry(setting).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return setting;
        }

        public int DeleteSetting(int id)
        {
            var settingToDelete = _context.Settings.Find(id);
            if (settingToDelete != null)
            {
                _context.Settings.Remove(settingToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
