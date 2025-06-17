using Microsoft.AspNetCore.Mvc;
using TaskMasterAPI.Services;

namespace TaskMasterAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Models.Task>> GetTasks()
    {
        return Ok(TaskDataStore.Current.Tasks);
    }

    [HttpGet("{id}")]
    public ActionResult<Models.Task> GetTask(int id)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        return Ok(task);
    }
}
