using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMasterAPI.Context;

namespace TaskMasterAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Task>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<Models.Task>> CreateTask(Models.TaskInsert taskInsert)
    {
        var newTask = new Models.Task
        {
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = taskInsert.Title,
            Description = taskInsert.Description
        };
        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
        return Ok(newTask);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Models.Task>> UpdateTask(int id, Models.TaskInsert taskInsert)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        task.Title = taskInsert.Title;
        task.Description = taskInsert.Description;
        task.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Models.Task>> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound("Task not found");
        }
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
