using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;

namespace taskmaster_api.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProfileEntity GetProfileById(int id)
        {
            return _context.Profiles.Find(id);
        }

        public IEnumerable<ProfileEntity> GetAllProfiles()
        {
            return _context.Profiles.ToList();
        }

        public ProfileEntity CreateProfile(ProfileEntity profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return profile;
        }

        public ProfileEntity UpdateProfile(int id, ProfileEntity profile)
        {
            if (_context.Profiles.Find(id) is ProfileEntity oldProfile)
            {
                profile.Id = id;
                _context.Profiles.Entry(oldProfile).State = EntityState.Detached;
                _context.Profiles.Entry(profile).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return profile;
        }

        public int DeleteProfile(int id)
        {
            var profileToDelete = _context.Profiles.Find(id);
            if (profileToDelete != null)
            {
                _context.Profiles.Remove(profileToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }

        public ProfileEntity GetProfileByUserId(string userId)
        {
            return _context.Profiles.FirstOrDefault(profile => profile.UserId == userId);
        }
    }
}
