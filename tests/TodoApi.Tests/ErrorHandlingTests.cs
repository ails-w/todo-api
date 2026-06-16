using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApi.Tests;

/// <summary>
/// Tests de integración para el middleware de error handling global (Fase 8).
/// Verifican que la API maneje errores de forma consistente y segura:
///   - Rutas inexistentes → 404
///   - Errores internos no controlados → 500 sin stack traces
///
/// Escritos con TDD: primero el test (falla), después la implementación.
/// </summary>
public class ErrorHandlingTests
{
    /// <summary>
    /// [TEST] Ruta inexistente → 404 NotFound.
    /// Verifica que una URL que no coincide con ningún endpoint devuelva 404.
    /// Sin el middleware, ASP.NET Core devuelve 404 sin Content-Type.
    /// Con el middleware, debe devolver 404 con cuerpo JSON.
    /// </summary>
    [Fact]
    public async Task Get_NonExistingRoute_Returns_404_NotFound()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/nonexistent-route");

        // Assert: status code 404
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        // Assert: respuesta en JSON (el middleware debe establecer Content-Type)
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
    }

    /// <summary>
    /// [TEST] Error interno → 500 sin datos sensibles.
    /// Verifica que una excepción no controlada en un endpoint devuelva:
    ///   - Status 500 InternalServerError
    ///   - Content-Type application/json
    ///   - Sin stack traces, nombres de excepción, ni información interna
    /// </summary>
    [Fact]
    public async Task Internal_Error_Returns_500_Without_SensitiveData()
    {
        // Arrange
        // Registramos un controller de test (en este mismo assembly) que lanza
        // una excepción, para poder probar el manejo de errores internos.
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddControllers()
                        .AddApplicationPart(typeof(ErrorTestController).Assembly);
                });
            });
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/test/internal-error");

        // Assert: 500 Internal Server Error
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        // Assert: respuesta en JSON
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

        // Assert: NO contiene datos sensibles
        var body = await response.Content.ReadAsStringAsync();
        Assert.DoesNotContain("StackTrace", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("InvalidOperationException", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("simulated internal error", body, StringComparison.OrdinalIgnoreCase);
    }
}

/// <summary>
/// Controller de test para simular errores internos del servidor.
/// Solo se registra en el contexto de tests via AddApplicationPart.
/// No forma parte del código de producción.
/// </summary>
[ApiController]
[Route("api/test")]
public class ErrorTestController : ControllerBase
{
    /// <summary>
    /// GET /api/test/internal-error — siempre lanza una excepción.
    /// Usado únicamente por Internal_Error_Returns_500_Without_SensitiveData.
    /// </summary>
    [HttpGet("internal-error")]
    public IActionResult GetInternalError()
    {
        throw new InvalidOperationException("Simulated internal error for testing");
    }
}
