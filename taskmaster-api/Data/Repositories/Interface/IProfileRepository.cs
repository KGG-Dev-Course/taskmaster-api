using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface IProfileRepository
    {
        ProfileEntity GetProfileById(int id);
        IEnumerable<ProfileEntity> GetAllProfiles();
        ProfileEntity CreateProfile(ProfileEntity profile);
        ProfileEntity UpdateProfile(int id, ProfileEntity profile);
        int DeleteProfile(int id);
        ProfileEntity GetProfileByUserId(string userId);
    }
}
