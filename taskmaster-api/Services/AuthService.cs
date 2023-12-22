using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using taskmaster_api.Data.Models;

namespace taskmaster_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        public ICoreActionResult<LoginResponseDto> Login(LoginDto loginDto)
        {
            try
            {
                var user = _userManager.FindByNameAsync(loginDto.Username).Result;
                if (user != null && _userManager.CheckPasswordAsync(user, loginDto.Password).Result)
                {
                    var userRoles = _userManager.GetRolesAsync(user).Result;

                    bool isAdmin = userRoles.Any(role => role == "Admin");

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    _logger.LogInformation("Login Successful.");

                    return CoreActionResult<LoginResponseDto>.Success(new LoginResponseDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        isAdmin = isAdmin
                    });
                }

                _logger.LogInformation("Login Failed.");
                return CoreActionResult<LoginResponseDto>.Failure("Login Failed.");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<LoginResponseDto>.Exception(ex);
            }
        }

        public ICoreActionResult<RegisterDto> Register(RegisterDto registerDto)
        {
            try
            {
                var userExists = _userManager.FindByNameAsync(registerDto.Username).Result;
                if (userExists != null)
                {
                    _logger.LogInformation("User already exists!");
                    return CoreActionResult<RegisterDto>.Failure("User already exists!");
                }

                IdentityUser user = new()
                {
                    Email = registerDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerDto.Username
                };
                var result = _userManager.CreateAsync(user, registerDto.Password).Result;

                if (!result.Succeeded)
                {
                    _logger.LogInformation("User creation failed! Please check user details and try again.");
                    return CoreActionResult<RegisterDto>.Failure("User creation failed! Please check user details and try again.");
                }

                if (!_roleManager.RoleExistsAsync(UserRoles.User).Result)
                {
                    var userIdentityResult = _roleManager.CreateAsync(new IdentityRole(UserRoles.User)).Result;
                }

                if (_roleManager.RoleExistsAsync(UserRoles.User).Result)
                {
                    var userIdentityResult = _userManager.AddToRoleAsync(user, UserRoles.User).Result;
                }

                _logger.LogInformation("Register Successful.");

                return CoreActionResult<RegisterDto>.Success(registerDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<RegisterDto>.Exception(ex);
            }
        }

        public ICoreActionResult<RegisterDto> RegisterAdmin(RegisterDto registerDto)
        {
            try
            {
                var userExists = _userManager.FindByNameAsync(registerDto.Username).Result;
                if (userExists != null)
                {
                    _logger.LogInformation("User already exists!");
                    return CoreActionResult<RegisterDto>.Failure("User already exists!");
                }

                IdentityUser user = new()
                {
                    Email = registerDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerDto.Username
                };

                var result = _userManager.CreateAsync(user, registerDto.Password).Result;
                if (!result.Succeeded)
                {
                    _logger.LogInformation("User creation failed! Please check user details and try again.");
                    return CoreActionResult<RegisterDto>.Failure("User creation failed! Please check user details and try again.");
                }

                if (!_roleManager.RoleExistsAsync(UserRoles.Admin).Result)
                {
                    var adminIdentityResult = _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).Result;
                }

                if (!_roleManager.RoleExistsAsync(UserRoles.User).Result)
                {
                    var userIdentityResult = _roleManager.CreateAsync(new IdentityRole(UserRoles.User)).Result;
                }

                if (_roleManager.RoleExistsAsync(UserRoles.Admin).Result)
                {
                    var adminIdentityResult = _userManager.AddToRoleAsync(user, UserRoles.Admin).Result;
                }

                if (_roleManager.RoleExistsAsync(UserRoles.User).Result)
                {
                    var userIdentityResult = _userManager.AddToRoleAsync(user, UserRoles.User).Result;
                }

                _logger.LogInformation("Admin Register Successful.");

                return CoreActionResult<RegisterDto>.Success(registerDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<RegisterDto>.Exception(ex);
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT:DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
