using System.Text.Json;

namespace TodoApi.Api.Middleware;

/// <summary>
/// Middleware global de manejo de errores.
///
/// Ubicación en el pipeline: lo más temprano posible (antes de cualquier otro middleware).
///
/// Responsabilidades:
///   1. Capturar <see cref="KeyNotFoundException"/> → 404 con cuerpo JSON.
///   2. Capturar cualquier otra excepción no controlada → 500 con cuerpo JSON genérico
///      (sin stack traces, sin información interna).
///   3. Si la respuesta es 404 y no tiene cuerpo (ruta inexistente, NotFound()),
///      escribir un cuerpo JSON consistente.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            // ── 404 sin cuerpo: rutas inexistentes o NotFound() sin body ──
            // Si el status es 404 y la respuesta aún no se ha enviado,
            // escribimos un cuerpo JSON consistente.
            if (context.Response.StatusCode == StatusCodes.Status404NotFound
                && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ErrorResponse("Not Found", 404), JsonOptions);
            }
        }
        catch (KeyNotFoundException)
        {
            // ── KeyNotFoundException → 404 ──
            // Útil cuando servicios lanzan esta excepción para indicar
            // que un recurso no existe.
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ErrorResponse("Not Found", 404), JsonOptions);
            }
        }
        catch (Exception)
        {
            // ── Error interno no controlado → 500 genérico ──
            // NO exponemos el mensaje de la excepción, stack trace,
            // ni ningún detalle interno.
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ErrorResponse("Internal Server Error", 500), JsonOptions);
            }
        }
    }
}

/// <summary>
/// DTO interno para respuestas de error uniformes.
/// Se serializa como JSON con camelCase.
/// </summary>
internal record ErrorResponse(string Error, int Status);
