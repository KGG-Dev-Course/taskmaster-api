using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Models;
using taskmaster_api.Services;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ApplicationControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return ToHttpResult<List<UserDto>>(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            return ToHttpResult<UserDto>(_userService.GetUserById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            return ToHttpResult(_userService.DeleteUser(id));
        }

        [HttpGet("mine")]
        public IActionResult GetMineUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return ToHttpResult<UserDto>(_userService.GetUserById(userId));
        }
    }
}
