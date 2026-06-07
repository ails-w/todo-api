# Controllers

## Qué son

Los **controllers** son clases que reciben requests HTTP y devuelven responses HTTP. Son la puerta de entrada de la API.

```csharp
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
```

## Anatomía de un controller

### `[ApiController]`

Habilita comportamientos automáticos:
- **Validación automática**: si un modelo recibido no es válido, devuelve `400 Bad Request` sin que escribas código
- **Binding automático**: infiere el origen de los parámetros (ruta, query, body) sin atributos extra
- **Atributo `[ApiController]` requerido**: Sin esto, tenés que validar manualmente con `if (!ModelState.IsValid)`

### `[Route("api/[controller]")]`

Define la URL base para todas las acciones del controller. `[controller]` es un wildcard que se reemplaza por el nombre del controller sin el sufijo `Controller`:

- `TasksController` → ruta base `/api/tasks`

### `ControllerBase`

Clase base para APIs (sin soporte de vistas). Provee helpers:

| Helper | Código HTTP |
|---|---|
| `Ok(obj)` | 200 OK |
| `NotFound()` | 404 NotFound |
| `BadRequest()` | 400 Bad Request |
| `CreatedAtAction("GetById", new { id }, obj)` | 201 Created |

## Acciones del controller

Cada método público es una **acción** que responde a un verbo HTTP:

```csharp
[HttpGet]                                      // GET /api/tasks
public async Task<ActionResult<List<TaskResponse>>> GetAll()

[HttpGet("{id:guid}")]                        // GET /api/tasks/{id}
public async Task<ActionResult<TaskResponse>> GetById(Guid id)
```

### `ActionResult<T>`

`ActionResult<T>` permite devolver **distintos tipos de respuesta** desde el mismo método:

```csharp
public async Task<ActionResult<TaskResponse>> GetById(Guid id)
{
    var task = await _taskService.GetByIdAsync(id);

    if (task is null)
        return NotFound();        // → 404

    return Ok(task);               // → 200 + JSON
}
```

Sin `ActionResult<T>`, tendrías que devolver `TaskResponse` siempre o `IActionResult` (perdiendo tipo).

## Inyección en controllers

Los controllers reciben sus dependencias por constructor, igual que cualquier otra clase:

```csharp
private readonly ITaskService _taskService;

public TasksController(ITaskService taskService)
{
    _taskService = taskService;
}
```

## El controller NO debe tener lógica de negocio

```csharp
// ❌ MAL: lógica de negocio en el controller
public async Task<ActionResult<List<TaskResponse>>> GetAll()
{
    var tasks = await SomeRawDataAccess();
    var filtered = tasks.Where(t => !t.IsCompleted).ToList();
    return Ok(filtered);
}

// ✅ BIEN: el controller solo delega
public async Task<ActionResult<List<TaskResponse>>> GetAll()
{
    var tasks = await _taskService.GetAllAsync();
    return Ok(tasks);
}
```

## Error común

- Poner lógica de negocio dentro del controller → difícil de probar y mantener.
- Devolver `TaskResponse` directamente en vez de `ActionResult<TaskResponse>` → perdés la capacidad de devolver 404 sin lanzar excepción.
- Olvidar `[ApiController]` → la validación no es automática.
- Usar `Controller` (con vistas) en vez de `ControllerBase` para APIs → incluye funcionalidad que no necesitás.

## Relación con el resto del sistema

```
HTTP Request → [Routing] → Controller → Service → Repository
                    ←         ←           ←
                JSON Response          DTOs      Modelo de dominio
```
