using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests;

/// <summary>
/// Prueba de humo: verifica que la API arranca y responde HTTP.
/// Si este test falla, nada más funciona.
/// </summary>
public class ApiHealthCheckTests
{
    [Fact]
    public async Task Get_ApiTasks_Returns_200_OK()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/tasks");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
