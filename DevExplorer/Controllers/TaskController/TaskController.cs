using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Interfaces.ITask;
using DevExplorerAPI.DevExplorer.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExplorerAPI.DevExplorer.Controllers.TaskController
{
    [ApiController]
    [Route("v1/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [Authorize]
        [HttpPost, Route("addTask")]
        public async Task<IActionResult> AddTaskAsync([FromBody] TaskModel model)
        {
            var task = await _taskService.AddTaskAsync(model);
            return Ok(task);
        }

        [Authorize]
        [HttpGet, Route("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }
    }
}