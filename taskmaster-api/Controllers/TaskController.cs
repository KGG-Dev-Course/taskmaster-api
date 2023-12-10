using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Models;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ApplicationControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            return ToHttpResult<TaskDto>(_taskService.GetTaskById(id));
        }

        [HttpPost]
        public IActionResult CreateTask(TaskDto taskDto)
        {
            return ToHttpResult<TaskDto>(_taskService.CreateTask(taskDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskDto taskDto)
        {
            return ToHttpResult<TaskDto>(_taskService.UpdateTask(id, taskDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            return ToHttpResult(_taskService.DeleteTask(id));
        }
    }
}
