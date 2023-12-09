using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
        }

        public ICoreActionResult<TaskDto> GetTaskById(int id)
        {
            try
            {
                var task = _taskRepository.GetTaskById(id);
                if (task == null)
                {
                    _logger.LogInformation("Task not found");
                    return CoreActionResult<TaskDto>.Failure("Task not found", "NotFound");
                }

                return CoreActionResult<TaskDto>.Success(task.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TaskDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<TaskDto>> GetAllTasks()
        {
            try
            {
                var tasks = _taskRepository.GetAllTasks();
                return CoreActionResult<List<TaskDto>>.Success(tasks.Select(task => task.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<TaskDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<TaskDto> CreateTask(TaskDto taskDto)
        {
            try
            {
                var task = taskDto.ToEntity();
                var newTask = _taskRepository.CreateTask(task);
                return CoreActionResult<TaskDto>.Success(newTask.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TaskDto>.Exception(ex);
            }
        }

        public ICoreActionResult<TaskDto> UpdateTask(int id, TaskDto taskDto)
        {
            try
            {
                var task = taskDto.ToEntity();
                var existingTask = _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    _logger.LogInformation("Task not found");
                    return CoreActionResult<TaskDto>.Failure("Task not found", "NotFound");
                }

                var updatedTask = _taskRepository.UpdateTask(id, task);
                return CoreActionResult<TaskDto>.Success(updatedTask.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TaskDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteTask(int id)
        {
            try
            {
                var deletedTaskId = _taskRepository.DeleteTask(id);
                if (deletedTaskId == 0)
                {
                    _logger.LogInformation("Task not found");
                    return CoreActionResult.Ignore("Task not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }
    }
}
