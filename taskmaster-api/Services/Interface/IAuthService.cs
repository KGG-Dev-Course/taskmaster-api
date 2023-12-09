using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;

namespace taskmaster_api.Services.Interface
{
    public interface IAuthService
    {
        ICoreActionResult<LoginResponseDto> Login(LoginDto loginDto);
        ICoreActionResult<RegisterDto> Register(RegisterDto registerDto);
        ICoreActionResult<RegisterDto> RegisterAdmin(RegisterDto registerDto);
    }
}
