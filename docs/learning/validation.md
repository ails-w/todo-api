# Validación con Data Annotations

## Qué son

Las **Data Annotations** son atributos de .NET que permiten declarar reglas de validación directamente sobre las propiedades de un modelo o DTO. ASP.NET Core las evalúa **automáticamente** durante el model binding, antes de que el código del controller se ejecute.

```csharp
public record CreateTaskRequest
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 200 caracteres.")]
    public string Title { get; init; } = string.Empty;

    public bool IsCompleted { get; init; }
}
```

## Cómo funciona en ASP.NET Core

### 1. El cliente envía datos

```
POST /api/tasks
Content-Type: application/json

{ "title": "", "isCompleted": false }
```

### 2. Model binding crea el objeto

ASP.NET Core deserializa el JSON en un `CreateTaskRequest` y **aplica las validaciones declaradas con atributos**.

### 3. Validación automática

Si alguna validación falla, el modelo se marca como inválido (`ModelState.IsValid == false`).

### 4. `[ApiController]` responde con 400

El atributo `[ApiController]` en el controller hace que ASP.NET Core **automáticamente** devuelva `400 Bad Request` con los errores de validación, sin que tengas que escribir código para eso.

```
HTTP/1.1 400 Bad Request
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "errors": {
        "Title": ["El título es obligatorio."]
    }
}
```

## Atributos más comunes

| Atributo | Qué valida | Ejemplo |
|---|---|---|
| `[Required]` | El valor no puede ser null ni empty | `[Required]` |
| `[StringLength(max)]` | Longitud máxima (opcional mínimo) | `[StringLength(200, MinimumLength = 1)]` |
| `[Range(min, max)]` | Valor numérico en un rango | `[Range(1, 100)]` |
| `[EmailAddress]` | Formato de email válido | `[EmailAddress]` |
| `[RegularExpression(pattern)]` | Coincide con una expresión regular | `[RegularExpression(@"^[A-Z]+$")]` |

## Por qué es mejor que validar manualmente

```csharp
// ❌ Manual — olvidable, repetitivo, mezcla lógica con validación
public async Task<ActionResult> Create(CreateTaskRequest request)
{
    if (string.IsNullOrWhiteSpace(request.Title))
        return BadRequest("El título es obligatorio.");

    if (request.Title.Length > 200)
        return BadRequest("El título no puede superar 200 caracteres.");
    // ...
}

// ✅ Declarativo — la validación está en el DTO, automática, centralizada
[HttpPost]
public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request)
{
    // Si llegamos acá, el request ya pasó validación
    var task = await _taskService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
}
```

## ModelState

Si necesitás acceso a los errores de validación (por ejemplo, para devolver un formato personalizado), usás `ModelState`:

```csharp
if (!ModelState.IsValid)
{
    var errores = ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage);
    return BadRequest(new { errores });
}
```

Con `[ApiController]` no hace falta — ya lo hace automáticamente.

## DTO de entrada con init

Usamos `{ get; init; }` en vez de `{ get; set; }` para que las propiedades sean **inmutables después de la creación**:

```csharp
public string Title { get; init; } = string.Empty;
```

Esto permite:

- **Object initializer**: `new CreateTaskRequest { Title = "Mi tarea" }`
- **Deserialización JSON**: ASP.NET Core puede escribirlo igual
- **Inmutabilidad**: una vez creado, no se puede modificar

## Error común

- Poner validación **solo en el frontend** y no en la API → cualquier cliente que no sea el frontend puede enviar datos inválidos.
- Validar manualmente en el controller en vez de usar Data Annotations → código repetitivo, fácil de olvidar.
- Usar `[Required]` en tipos por valor (`int`, `bool`, `Guid`) sin marcarlos como `nullable` → el valor por defecto (0, false, Guid.Empty) **no falla** la validación.

## Relación con el resto del sistema

```
Cliente → POST /api/tasks (JSON)
              ↓
         Model Binding → CreateTaskRequest
              ↓
         Data Annotations validan ← automático
              ↓
         ¿ModelState.IsValid?
           ├─ No → 400 Bad Request (automático)
           └─ Sí → Controller → Service → Repository → 201 Created
```
