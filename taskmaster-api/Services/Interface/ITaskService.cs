using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;

namespace taskmaster_api.Services.Interface
{
    public interface ITaskService
    {
        ICoreActionResult<TaskDto> GetTaskById(int id);
        ICoreActionResult<List<TaskDto>> GetAllTasks();
        ICoreActionResult<TaskDto> CreateTask(TaskDto taskDto);
        ICoreActionResult<TaskDto> UpdateTask(int id, TaskDto taskDto);
        ICoreActionResult DeleteTask(int id);
    }
}
