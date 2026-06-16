# Middleware en ASP.NET Core

## ¿Qué es?

Un **middleware** es un componente que se ejecuta en el pipeline de la aplicación. Cada middleware puede:

- Procesar el request entrante antes de pasarlo al siguiente
- Cortar el flujo y devolver una respuesta directamente
- Modificar la respuesta saliente después de que el pipeline la generó

```
Request → [MW1] → [MW2] → [MW3] → Controller → Response
                ←        ←        ←
```

Cada middleware decide si llama a `_next(context)` para seguir o devuelve ya.

## ¿Para qué sirve?

Para **separar preocupaciones transversales** del negocio:

| Middleware | Responsabilidad |
|-----------|-----------------|
| Exception handling | Capturar errores no controlados y responder con formato uniforme |
| Logging | Registrar cada request/response |
| Autenticación | Verificar identidad del cliente |
| Autorización | Verificar permisos |
| CORS | Permitir/denegar orígenes cruzados |
| Compresión | Comprimir respuestas |
| Static files | Servir archivos estáticos |

## ¿Cómo se usa aquí?

En `src/TodoApi.Api/Middleware/ExceptionMiddleware.cs`:

```csharp
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // ← pasa al siguiente middleware

            // Post-procesamiento: si la respuesta es 404 sin cuerpo,
            // escribimos JSON consistente.
        }
        catch (KeyNotFoundException)
        {
            // KeyNotFoundException → 404 JSON
        }
        catch (Exception)
        {
            // Cualquier otra excepción → 500 JSON (sin stack trace)
        }
    }
}
```

Se registra en `Program.cs` como el **primer middleware del pipeline**:

```csharp
app.UseMiddleware<ExceptionMiddleware>();
```

Esto asegura que capture excepciones de TODOS los middlewares posteriores (Swagger, routing, controllers, etc.).

## Error común

**Poner el middleware después de otros.** Si el middleware de error handling se registra después de `MapControllers()`, las excepciones lanzadas en controllers nunca pasan por él. El orden de registro define el orden de ejecución:

```csharp
// ❌ MAL: el middleware no capturará excepciones de controllers
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

// ✅ BIEN: el middleware envuelve todo el pipeline
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
```

## Relación con el resto del proyecto

El `ExceptionMiddleware` reemplazó la necesidad de tener try/catch en cada controller o servicio. Ahora:

- **Controllers** se enfocan solo en la lógica HTTP (routing, status codes)
- **Services** se enfocan solo en la lógica de negocio
- **Middleware** maneja la transformación de excepciones a respuestas HTTP

Esto sigue el principio de **separación de responsabilidades** y elimina código repetitivo.

## Excepciones vs resultados HTTP

| Excepción | Status HTTP | Cuándo ocurre |
|-----------|-------------|---------------|
| `KeyNotFoundException` | 404 | Recurso no encontrado (servicio/repositorio) |
| `ArgumentException` / `ValidationException` | 400 | Datos inválidos (validación) |
| Cualquier otra `Exception` no controlada | 500 | Error interno del servidor |

## Seguridad: no exponer stack traces

Un stack trace revela:
- Rutas absolutas del servidor (`/home/user/project/...`)
- Nombres de clases y métodos internos
- Versiones de librerías y frameworks

Por eso nuestro middleware captura `Exception` y responde con un mensaje genérico:

```json
{ "error": "Internal Server Error", "status": 500 }
```

Sin exponer el mensaje real de la excepción, el tipo, ni el stack trace. El error REAL se loguea internamente (o se registra en un sistema de monitoreo), pero nunca llega al cliente.

## Referencias

- [Documentación oficial: Middleware en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/)
- [Código fuente: ExceptionMiddleware](../../src/TodoApi.Api/Middleware/ExceptionMiddleware.cs)
