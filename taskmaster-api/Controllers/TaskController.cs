using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ApplicationControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            return ToHttpResult<TaskDto>(_taskAppService.GetTaskById(id));
        }

        [HttpPost]
        public IActionResult CreateTask(TaskDto taskDto)
        {
            return ToHttpResult<TaskDto>(_taskAppService.CreateTask(taskDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskDto taskDto)
        {
            return ToHttpResult<TaskDto>(_taskAppService.UpdateTask(id, taskDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            return ToHttpResult(_taskAppService.DeleteTask(id));
        }
    }
}
