# JSON

## Qué es

JSON (JavaScript Object Notation) es el formato de intercambio de datos que usa la API para todas las requests y responses.

## Cómo se usa en este proyecto

### Serializador por defecto

ASP.NET Core usa **`System.Text.Json`** como serializador JSON por defecto. No fue necesario agregar configuración explícita en `Program.cs` porque los defaults ya hacen lo que necesitamos:

- **camelCase**: todas las propiedades se serializan en minúscula con notación camello (`title`, `isCompleted`, `error`, `status`)
- **Records**: los DTOs están definidos como `record` (inmutables), y `System.Text.Json` los serializa/deserializa sin problemas
- **Guid → string**: los `Guid` se serializan automáticamente como strings ("3fa85f64-5717-4562-b3fc-2c963f66afa6")

### DTOs de entrada (deserialización)

El cliente envía JSON en el body del request, y ASP.NET Core lo deserializa automáticamente al DTO correspondiente:

```json
{
  "title": "Comprar pan",
  "isCompleted": false
}
```

Si el JSON no cumple con las reglas de validación (`[Required]`, `[StringLength]`), ASP.NET Core devuelve **400 Bad Request** automáticamente sin que el controller tenga que hacer nada. Si el JSON está malformado (typo, tipo incorrecto), también devuelve 400.

### DTOs de salida (serialización)

El controller devuelve `Ok(task)`, `CreatedAtAction(...)`, etc., y ASP.NET Core serializa el objeto a JSON automáticamente:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Comprar pan",
  "isCompleted": false
}
```

### Respuestas de error uniformes

El `ExceptionMiddleware` construye y devuelve respuestas JSON consistentes para todos los errores:

```json
{
  "error": "Not Found",
  "status": 404
}
```

Esto se logra con el record `ErrorResponse` y `WriteAsJsonAsync`, siempre con `Content-Type: application/json`.

### Content-Type

Todas las respuestas JSON usan `application/json`, que es el estándar oficial (RFC 8259). Nunca se usa `text/json` ni `text/plain`.

## Cómo verifica Swagger los schemas JSON

Swagger genera un documento `/swagger/v1/swagger.json` que describe todos los schemas de la API, incluyendo los tipos de datos JSON. Los tests verifican que los DTOs aparezcan en ese documento con sus propiedades correspondientes.

## Errores comunes

- **No alinear nombres, tipos o estructuras entre el contrato y la implementación**: el DTO en C# define la estructura JSON, pero hay que asegurarse de que coincida con lo que el cliente espera
- **JSON malformado**: si el cliente envía `Title` (con mayúscula) en vez de `title` (camelCase), ASP.NET Core igual lo acepta porque `System.Text.Json` es **case-insensitive** por defecto para propiedades
- **Olvidar que los DTOs records inmutables no tienen `set`**: se usa `init` en lugar de `set` para mantener inmutabilidad post-creación

## Relación con el resto del sistema

| Componente | Relación con JSON |
|---|---|
| DTOs de entrada (`CreateTaskRequest`, `UpdateTaskRequest`) | Se deserializan desde JSON del body del request |
| DTOs de salida (`TaskResponse`) | Se serializan a JSON en la respuesta HTTP |
| `ErrorResponse` | Record interno para respuestas de error JSON uniformes |
| `ExceptionMiddleware` | Escribe JSON directamente con `WriteAsJsonAsync` |
| Swagger/OpenAPI | Genera `swagger.json` con schemas de todos los DTOs |
| Tests | Verifican que las responses sean JSON válido con `Content-Type: application/json` |

## Referencias

- [`ExceptionMiddleware.cs`](../../src/TodoApi.Api/Middleware/ExceptionMiddleware.cs) — uso de `System.Text.Json` en el middleware
- [`CreateTaskRequest.cs`](../../src/TodoApi.Api/Features/Tasks/Dtos/CreateTaskRequest.cs) — DTO que se deserializa desde JSON
- [`TaskResponse.cs`](../../src/TodoApi.Api/Features/Tasks/Dtos/TaskResponse.cs) — DTO que se serializa a JSON
- [`SwaggerEndpointTests.cs`](../../tests/TodoApi.Tests/SwaggerEndpointTests.cs) — tests que verifican schemas JSON en Swagger
- [Documentación oficial de System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)
