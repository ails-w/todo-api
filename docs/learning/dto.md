# DTOs (Data Transfer Objects)

## Qué son

Los DTOs son objetos que **transportan datos** entre capas de la aplicación, sin lógica de negocio. Su única responsabilidad es llevar información de un lugar a otro.

## DTO de salida vs DTO de entrada

| Tipo | Propósito | Ejemplo en Fase 2 |
|---|---|---|
| **DTO de salida** | Lo que la API **devuelve** al cliente | `TaskResponse` |
| **DTO de entrada** | Lo que la API **recibe** del cliente | `CreateTaskRequest` (Fase 3) |

## Por qué separar DTOs del modelo de dominio

```csharp
// Modelo de dominio — interno, puede cambiar
public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

// DTO de salida — contrato público, estable
public record TaskResponse(Guid Id, string Title, bool IsCompleted);
```

- El **modelo de dominio** (`TaskItem`) es interno. Puede crecer con campos como `CreatedAt`, `Priority`, etc.
- El **DTO** (`TaskResponse`) es el contrato público. Una vez que los clientes dependen de él, cambiarlo es más costoso.
- Si en el futuro agregás `CreatedAt` al dominio pero no querés exponerlo, el DTO te protege: simplemente no lo mapeás.

## Validación en DTOs de entrada

Los DTOs de entrada pueden declarar reglas de validación con **Data Annotations**:

```csharp
public record CreateTaskRequest
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 200 caracteres.")]
    public string Title { get; init; } = string.Empty;

    public bool IsCompleted { get; init; }
}
```

Con `[ApiController]`, ASP.NET Core valida automáticamente el DTO al recibirlo y devuelve `400 Bad Request` si falla. No necesitas escribir `if (!ModelState.IsValid)`.

Ver más en `docs/learning/validation.md`.

## Record vs Class para DTOs

Usamos `record` en vez de `class` porque:

```csharp
public record TaskResponse(Guid Id, string Title, bool IsCompleted);
```

| Característica | `class` | `record` |
|---|---|---|
| Mutabilidad | Mutable por defecto | Inmutable (solo `init`) |
| Comparación | Por referencia | Por valor |
| `ToString()` | `Namespace.ClassName` | Muestra todas las propiedades |
| Sintaxis | Requiere escribir propiedades | Posicional: `record X(int A, string B)` |

Un DTO es **inmutable por naturaleza**: solo transporta datos, nunca debería modificarse después de creado. `record` lo expresa en el tipo mismo.

## Mapeo dominio → DTO

El mapeo se hace en el Service, no en el Controller ni en el Repository:

```csharp
public async Task<List<TaskResponse>> GetAllAsync()
{
    var tasks = await _repository.GetAllAsync();            // TaskItem (dominio)
    return tasks.Select(t => new TaskResponse(              // → TaskResponse (DTO)
        t.Id, t.Title, t.IsCompleted
    )).ToList();
}
```

## Error común

- Exponer el modelo de dominio directamente como respuesta → cualquier cambio interno rompe el contrato público.
- Poner lógica de negocio en un DTO → los DTOs no tienen comportamiento.
- Usar el mismo objeto para entrada y salida → terminás exponiendo campos que no deberías o recibiendo campos que no esperabas.

## Relación con el resto del sistema

```
Controller ↔ Service (DTOs) ↔ Repository (modelo de dominio)
                ↑
           mapeo manual
```
