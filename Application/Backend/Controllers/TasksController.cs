using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Dtos;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // Statyczna lista udająca bazę danych na potrzeby Zadania 5
    private static List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Name = "Zadanie 1", IsCompleted = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskReadDto>> GetAll()
    {
        var dtos = _tasks.Select(t => new TaskReadDto 
        { 
            Id = t.Id, 
            Name = t.Name, 
            IsCompleted = t.IsCompleted 
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskReadDto> GetById(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        var dto = new TaskReadDto 
        { 
            Id = task.Id, 
            Name = task.Name, 
            IsCompleted = task.IsCompleted 
        };
        return Ok(dto);
    }

    [HttpPost]
    public ActionResult Create(TaskItem task)
    {
        // Automatyczne nadawanie ID
        task.Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        
        // Ustawienie daty utworzenia (widocznej tylko w bazie/encji, nie w DTO)
        task.CreatedAt = DateTime.UtcNow;
        
        _tasks.Add(task);
        
        // Zwracamy status 201 Created
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
}