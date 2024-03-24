using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Interfaces.ITask;
using DevExplorerAPI.DevExplorer.Models.Task;

namespace DevExplorerAPI.DevExplorer.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel model)
        {
            var task = await _taskRepository.AddTaskAsync(model);
            return task;
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            var tasks = await _taskRepository.GetTasks();
            return tasks;
        }
    }
}