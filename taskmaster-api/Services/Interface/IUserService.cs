using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface IUserService
    {
        ICoreActionResult<UserDto> GetUserById(string id);
        ICoreActionResult<List<UserDto>> GetAllUsers();
        ICoreActionResult DeleteUser(string id);
    }
}
