# HTTP Methods y Códigos de Respuesta

## Qué son

Los **métodos HTTP** expresan la intención de la acción. Los **códigos de respuesta** indican el resultado.

## Métodos HTTP

| Método | Intención | En nuestra API |
|---|---|---|
| `GET` | Leer/recuperar datos | `GET /api/tasks` — lista de tareas |
| `POST` | Crear un recurso | Fase 3: `POST /api/tasks` — crear tarea |
| `PUT` | Reemplazar/actualizar | Fase 4: `PUT /api/tasks/{id}` — actualizar tarea |
| `DELETE` | Borrar un recurso | Fase 6: `DELETE /api/tasks/{id}` — eliminar tarea |

## Códigos de respuesta

| Código | Significado | ¿Cuándo se usa? |
|---|---|---|
| `200 OK` | La request se procesó correctamente y devuelve datos | `GET /api/tasks` → lista de tareas |
| `200 OK` | Recurso encontrado y devuelto | `GET /api/tasks/{id}` → tarea individual |
| `201 Created` | Recurso creado exitosamente | `POST /api/tasks` → tarea nueva (Fase 3) |
| `204 NoContent` | Éxito sin body | `DELETE /api/tasks/{id}` → tarea eliminada (Fase 6) |
| `400 Bad Request` | La request es inválida (validación o formato) | Ruta con GUID inválido o body inválido |
| `404 Not Found` | El recurso solicitado no existe | `GET /api/tasks/{id}` o `PUT /api/tasks/{id}` con ID inexistente |

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

// 200 OK si existe, 404 si no (PUT actualizar)
[HttpPut("{id:guid}")]
public async Task<ActionResult<TaskResponse>> Update(Guid id, [FromBody] UpdateTaskRequest request)
{
    var task = await _taskService.UpdateAsync(id, request);

    if (task is null)
        return NotFound();                         // 404

    return Ok(task);                               // 200 + JSON actualizado
}

// 204 NoContent si existe, 404 si no (DELETE)
[HttpDelete("{id:guid}")]
public async Task<IActionResult> Delete(Guid id)
{
    var deleted = await _taskService.DeleteAsync(id);

    if (!deleted)
        return NotFound();                          // 404

    return NoContent();                             // 204 sin body
}

// 201 Created con Location header
[HttpPost]
public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request)
{
    var task = await _taskService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    //         ↑ 201 Created
    //         Location: /api/tasks/{id}
    //         Body: TaskResponse
}
```

### CreatedAtAction en detalle

`CreatedAtAction(actionName, routeValues, body)`:

| Parámetro | Qué genera | Ejemplo |
|---|---|---|
| `actionName` | Nombre del action que devuelve el recurso | `nameof(GetById)` → `"GetById"` |
| `routeValues` | Parámetros de ruta para la Location URL | `new { id = task.Id }` → `{id}` en la ruta |
| `body` | El recurso creado en el body de la respuesta | `task` (TaskResponse) |

Resultado HTTP:
```
HTTP/1.1 201 Created
Location: /api/Tasks/4f6e8e05-fea9-4870-bd57-123456789abc
Content-Type: application/json

{
    "id": "4f6e8e05-fea9-4870-bd57-123456789abc",
    "title": "Nueva tarea",
    "isCompleted": false
}
```

### Helpers de ControllerBase

| Helper | HTTP | Uso |
|---|---|---|
| `Ok(obj)` | 200 | Éxito con body |
| `CreatedAtAction(name, routeValues, obj)` | 201 | Recurso creado + Location header |
| `NoContent()` | 204 | Éxito sin body |
| `BadRequest()` | 400 | Datos inválidos (validación) |
| `NotFound()` | 404 | Recurso no encontrado |

## Por qué 404 y no null

Cuando `GetByIdAsync` devuelve `null`, el controller **decide** qué código HTTP usar. Devuelve **404 NotFound** porque desde la perspectiva HTTP el recurso no existe. Si devolvieras `Ok(null)` sería 200 con body null, lo cual confunde al cliente.

## Error común

- Usar el método HTTP incorrecto: `GET` para crear recursos, `POST` para leer.
- Devolver `200` cuando el recurso no existe (debería ser `404`).
- Devolver `500 Internal Server Error` para errores de validación del cliente (debería ser `400`).
- Olvidar el `Location header` en un 201 Created — el cliente no sabe cómo obtener el recurso creado.
- No documentar los códigos de respuesta posibles para cada endpoint.

## Relación con el resto del sistema

```
Cliente → GET /api/tasks → 200 [TaskResponse, TaskResponse, ...]
Cliente → GET /api/tasks/{id-existe} → 200 [TaskResponse]
Cliente → GET /api/tasks/{id-no-existe} → 404
Cliente → GET /api/tasks/abc → 400 (GUID inválido por route constraint)
Cliente → POST /api/tasks (válido) → 201 + Location: /api/tasks/{id} + TaskResponse
Cliente → POST /api/tasks (inválido) → 400 + errores de validación
Cliente → PUT /api/tasks/{id-existe} → 200 + TaskResponse actualizado
Cliente → PUT /api/tasks/{id-no-existe} → 404
Cliente → PUT /api/tasks/{id} (inválido) → 400 + errores de validación
Cliente → DELETE /api/tasks/{id-existe} → 204
Cliente → DELETE /api/tasks/{id-no-existe} → 404
```
