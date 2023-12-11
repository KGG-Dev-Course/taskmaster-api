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
    public class NotificationController : ApplicationControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetAllNotifications()
        {
            return ToHttpResult<List<NotificationDto>>(_notificationService.GetAllNotifications());
        }

        [HttpGet("{id}")]
        public IActionResult GetNotification(int id)
        {
            return ToHttpResult<NotificationDto>(_notificationService.GetNotificationById(id));
        }

        [HttpPost]
        public IActionResult CreateNotification(NotificationDto notificationDto)
        {
            return ToHttpResult<NotificationDto>(_notificationService.CreateNotification(notificationDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNotification(int id, NotificationDto notificationDto)
        {
            return ToHttpResult<NotificationDto>(_notificationService.UpdateNotification(id, notificationDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            return ToHttpResult(_notificationService.DeleteNotification(id));
        }
    }
}
