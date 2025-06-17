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

    [HttpPost]
    public ActionResult<Models.Task> CreateTask(Models.TaskInsert taskInsert)
    {
        var newTask = new Models.Task
        {
            Id = TaskDataStore.Current.Tasks.Max(t => t.Id) + 1,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = taskInsert.Title,
            Description = taskInsert.Description
        };
        TaskDataStore.Current.Tasks.Add(newTask);
        return Ok(newTask);
    }

    [HttpPut("{id}")]
    public ActionResult<Models.Task> UpdateTask(int id, Models.TaskInsert taskInsert)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        task.Title = taskInsert.Title;
        task.Description = taskInsert.Description;
        task.UpdatedAt = DateTime.Now;
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public ActionResult<Models.Task> DeleteTask(int id)
    {
        var task = TaskDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        TaskDataStore.Current.Tasks.Remove(task);
        return NoContent();
    }
}
