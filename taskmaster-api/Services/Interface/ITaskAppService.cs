using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;

namespace taskmaster_api.Services.Interface
{
    public interface ITaskAppService
    {
        ICoreActionResult<TaskDto> GetTaskById(int id);
        ICoreActionResult<List<TaskDto>> GetAllTasks();
        ICoreActionResult<TaskDto> CreateTask(TaskDto task);
        ICoreActionResult<TaskDto> UpdateTask(int id, TaskDto task);
        ICoreActionResult DeleteTask(int id);
    }
}
