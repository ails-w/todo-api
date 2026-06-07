# HTTP Methods y Códigos de Respuesta

## Qué son

Los **métodos HTTP** expresan la intención de la acción. Los **códigos de respuesta** indican el resultado.

## Métodos HTTP

| Método | Intención | En nuestra API |
|---|---|---|
| `GET` | Leer/recuperar datos | `GET /api/tasks` — lista de tareas |
| `POST` | Crear un recurso | Fase 3: `POST /api/tasks` — crear tarea |
| `PUT` | Reemplazar/actualizar | Fase 4: `PUT /api/tasks/{id}` — actualizar tarea |
| `DELETE` | Borrar un recurso | Fase 5: `DELETE /api/tasks/{id}` — eliminar tarea |

## Códigos de respuesta en Fase 2

| Código | Significado | ¿Cuándo se usa? |
|---|---|---|
| `200 OK` | La request se procesó correctamente y devuelve datos | `GET /api/tasks` → lista de tareas |
| `200 OK` | Recurso encontrado y devuelto | `GET /api/tasks/{id}` → tarea individual |
| `404 Not Found` | El recurso solicitado no existe | `GET /api/tasks/{id}` con ID inexistente |
| `400 Bad Request` | La request es inválida | Ruta con GUID inválido (`GET /api/tasks/abc`) |

## Cómo se devuelven en el controller

```csharp
// 200 OK con body
[HttpGet]
public async Task<ActionResult<List<TaskResponse>>> GetAll()
{
    var tasks = await _taskService.GetAllAsync();
    return Ok(tasks);                              // 200 + JSON
}

// 200 OK si existe, 404 si no
[HttpGet("{id:guid}")]
public async Task<ActionResult<TaskResponse>> GetById(Guid id)
{
    var task = await _taskService.GetByIdAsync(id);

    if (task is null)
        return NotFound();                         // 404

    return Ok(task);                               // 200 + JSON
}
```

### Helpers de ControllerBase

| Helper | HTTP | Uso |
|---|---|---|
| `Ok(obj)` | 200 | Éxito con body |
| `NotFound()` | 404 | Recurso no encontrado |
| `BadRequest()` | 400 | Datos inválidos |
| `CreatedAtAction(...)` | 201 | Recurso creado (Fase 3) |
| `NoContent()` | 204 | Éxito sin body (Fase 5 DELETE) |

## Por qué 404 y no null

Cuando `GetByIdAsync` devuelve `null`, el controller **decide** qué código HTTP usar. Devuelve **404 NotFound** porque desde la perspectiva HTTP el recurso no existe. Si devolvieras `Ok(null)` sería 200 con body null, lo cual confunde al cliente.

## Error común

- Usar el método HTTP incorrecto: `GET` para crear recursos, `POST` para leer.
- Devolver `200` cuando el recurso no existe (debería ser `404`).
- Devolver `500 Internal Server Error` para errores de validación del cliente (debería ser `400`).
- No documentar los códigos de respuesta posibles para cada endpoint.

## Relación con el resto del sistema

```
Cliente → GET /api/tasks → 200 [TaskResponse, TaskResponse, ...]
Cliente → GET /api/tasks/{id-existe} → 200 [TaskResponse]
Cliente → GET /api/tasks/{id-no-existe} → 404
Cliente → GET /api/tasks/abc → 400 (GUID inválido por route constraint)
```
