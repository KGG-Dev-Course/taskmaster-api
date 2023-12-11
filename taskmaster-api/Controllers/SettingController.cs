using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingController : ApplicationControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public IActionResult GetAllSettings()
        {
            return ToHttpResult<List<SettingDto>>(_settingService.GetAllSettings());
        }

        [HttpGet("{id}")]
        public IActionResult GetSetting(int id)
        {
            return ToHttpResult<SettingDto>(_settingService.GetSettingById(id));
        }

        [HttpPost]
        public IActionResult CreateSetting(SettingDto settingDto)
        {
            return ToHttpResult<SettingDto>(_settingService.CreateSetting(settingDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSetting(int id, SettingDto settingDto)
        {
            return ToHttpResult<SettingDto>(_settingService.UpdateSetting(id, settingDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSetting(int id)
        {
            return ToHttpResult(_settingService.DeleteSetting(id));
        }
    }
}
