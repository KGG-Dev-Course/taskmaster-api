using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApplicationControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            return ToHttpResult<LoginResponseDto>(_authService.Login(loginDto));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto registerDto)
        {
            return ToHttpResult<RegisterDto>(_authService.Register(registerDto));
        }

        [HttpPost("register-admin")]
        [AllowAnonymous]
        public IActionResult RegisterAdmin(RegisterDto registerDto)
        {
            return ToHttpResult<RegisterDto>(_authService.RegisterAdmin(registerDto));
        }
    }
}
