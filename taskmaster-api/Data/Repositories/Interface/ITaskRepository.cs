using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ITaskRepository
    {
        TaskEntity GetTaskById(int id);
        IEnumerable<TaskEntity> GetAllTasks();
        TaskEntity CreateTask(TaskEntity task);
        TaskEntity UpdateTask(int id, TaskEntity task);
        int DeleteTask(int id);
    }
}
