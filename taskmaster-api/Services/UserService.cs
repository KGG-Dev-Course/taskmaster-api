using Microsoft.AspNetCore.Identity;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<TicketService> _logger;

        public UserService(UserManager<IdentityUser> userManager, ILogger<TicketService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public ICoreActionResult DeleteUser(string id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return CoreActionResult.Failure("User not found", "NotFound");
                }

                var result = _userManager.DeleteAsync(user).Result;

                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }

        public ICoreActionResult<List<UserDto>> GetAllUsers()
        {
            try
            {
                List<UserDto> userDtoList = new List<UserDto>();
                foreach (var user in _userManager.Users)
                {
                    userDtoList.Add(new UserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email
                    });
                }

                return CoreActionResult<List<UserDto>>.Success(userDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<UserDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<UserDto> GetUserById(string id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;

                if (user == null)
                {
                    _logger.LogInformation("User not found");
                    return CoreActionResult<UserDto>.Failure("User not found", "NotFound");
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };

                return CoreActionResult<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<UserDto>.Exception(ex);
            }
        }
    }
}
