using Microsoft.AspNetCore.Mvc;
using TodoApi.Api.Features.Tasks.Dtos;
using TodoApi.Api.Features.Tasks.Services;

namespace TodoApi.Api.Controllers;

/// <summary>
/// Endpoints públicos de la API para el feature Tasks.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// GET /api/tasks — devuelve todas las tareas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// POST /api/tasks — crea una nueva tarea.
    /// Devuelve 201 Created con Location header y la tarea creada.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request)
    {
        var task = await _taskService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    /// <summary>
    /// PUT /api/tasks/{id} — actualiza una tarea existente.
    /// 200 OK con la tarea actualizada, 404 si no existe.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> Update(Guid id, [FromBody] UpdateTaskRequest request)
    {
        var task = await _taskService.UpdateAsync(id, request);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    /// <summary>
    /// GET /api/tasks/{id} — devuelve una tarea por ID.
    /// 404 si no existe.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetById(Guid id)
    {
        var task = await _taskService.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }
}
