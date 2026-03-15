using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly List<TaskItem> _tasks;

    public TasksController(List<TaskItem> tasks)
    {
        _tasks = tasks;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll() => Ok(_tasks);

    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public ActionResult Create(TaskItem task)
    {
        task.Id = _tasks.Max(t => t.Id) + 1;
        _tasks.Add(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskItem task)
    {
        var existing = _tasks.FirstOrDefault(t => t.Id == id);
        if (existing == null) return NotFound();
        existing.Name = task.Name;
        existing.IsCompleted = task.IsCompleted;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();
        _tasks.Remove(task);
        return NoContent();
    }
}