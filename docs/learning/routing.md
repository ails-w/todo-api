# Routing

## Qué es

El **routing** decide qué controller y qué acción responden a una request HTTP entrante.

## Routing por atributos

En Fase 2 usamos **attribute routing**: las rutas se definen con atributos C# directamente en el controller y sus acciones.

```csharp
[Route("api/[controller]")]     // ← ruta base del controller
public class TasksController : ControllerBase
{
    [HttpGet]                           // GET /api/tasks
    public async Task<ActionResult<List<TaskResponse>>> GetAll()

    [HttpGet("{id:guid}")]              // GET /api/tasks/{id}
    public async Task<ActionResult<TaskResponse>> GetById(Guid id)
}
```

### `[Route("api/[controller]")]`

- Define el prefijo común para todas las acciones del controller
- `[controller]` es un **reemplazo automático**: `TasksController` → `tasks`
- Resultado: todas las rutas empiezan con `/api/tasks`

### `[HttpGet]`, `[HttpPost]`, etc.

Cada verbo HTTP tiene su propio atributo. Mapean el método HTTP al método de C#:

| Atributo | Método HTTP |
|---|---|
| `[HttpGet]` | GET |
| `[HttpPost]` | POST |
| `[HttpPut]` | PUT |
| `[HttpDelete]` | DELETE |

## Route constraints

Los **constraints** restringen qué valores acepta un parámetro de ruta:

```csharp
[HttpGet("{id:guid}")]
public async Task<ActionResult<TaskResponse>> GetById(Guid id)
```

`{id:guid}` significa: _"el parámetro `id` debe ser un GUID válido"_.

| Constraint | Ejemplo | ¿Qué valida? |
|---|---|---|
| `:guid` | `{id:guid}` | GUID válido |
| `:int` | `{id:int}` | Número entero |
| `:alpha` | `{name:alpha}` | Solo letras |
| `:min(1)` | `{id:int:min(1)}` | Valor mínimo |

Si la request no cumple el constraint, ASP.NET devuelve **400 Bad Request** automáticamente.

## Cómo activar el routing

En `Program.cs`:

```csharp
builder.Services.AddControllers();   // ← registra los controllers en DI
app.MapControllers();                  // ← activa el routing por atributos
```

Sin `AddControllers()`, los controllers no existen como servicios.  
Sin `MapControllers()`, las rutas definidas con atributos no se evalúan.

## Error común

- Olvidar `app.MapControllers()` → las rutas no funcionan, todas las requests devuelven 404.
- No usar constraints → `{id}` acepta cualquier string, perdés validación temprana.
- Rutas inconsistentes: mezclar `api/tasks` con `api/tasks/` o `tasks/` → confunde a los clientes.
- No entender que `[controller]` se reemplaza por el nombre de la clase → si renombrás el controller, cambiás la ruta.

## Relación con el resto del sistema

```
URL entrante → Routing → Controller → Acción
  /api/tasks/123 → [Route("api/[controller]")] + [HttpGet("{id:guid}")]
                    → TasksController.GetById(Guid id)
```
