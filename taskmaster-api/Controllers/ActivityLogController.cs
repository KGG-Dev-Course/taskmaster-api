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
    public class ActivityLogController : ApplicationControllerBase
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        [HttpGet]
        public IActionResult GetAllActivityLogs()
        {
            return ToHttpResult<List<ActivityLogDto>>(_activityLogService.GetAllActivityLogs());
        }

        [HttpGet("{id}")]
        public IActionResult GetActivityLog(int id)
        {
            return ToHttpResult<ActivityLogDto>(_activityLogService.GetActivityLogById(id));
        }

        [HttpPost]
        public IActionResult CreateActivityLog(ActivityLogDto activityLogDto)
        {
            return ToHttpResult<ActivityLogDto>(_activityLogService.CreateActivityLog(activityLogDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActivityLog(int id, ActivityLogDto activityLogDto)
        {
            return ToHttpResult<ActivityLogDto>(_activityLogService.UpdateActivityLog(id, activityLogDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActivityLog(int id)
        {
            return ToHttpResult(_activityLogService.DeleteActivityLog(id));
        }
    }
}
