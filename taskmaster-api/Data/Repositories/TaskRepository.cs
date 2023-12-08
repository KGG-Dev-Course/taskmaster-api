using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace taskmaster_api.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TaskEntity GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public IEnumerable<TaskEntity> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public TaskEntity CreateTask(TaskEntity task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public TaskEntity UpdateTask(int id, TaskEntity task)
        {
            if (_context.Tasks.Find(id) is TaskEntity oldTask)
            {
                task.Id = id;
                _context.Tasks.Entry(oldTask).State = EntityState.Detached;
                _context.Tasks.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return task;
        }

        public int DeleteTask(int id)
        {
            var taskToDelete = _context.Tasks.Find(id);
            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
