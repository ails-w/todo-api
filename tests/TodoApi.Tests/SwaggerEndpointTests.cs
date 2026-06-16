using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApi.Tests;

/// <summary>
/// Tests de integración para verificar la configuración de Swagger / OpenAPI.
/// Validan que el endpoint OpenAPI esté disponible y que documente todos los endpoints de la API.
/// </summary>
public class SwaggerEndpointTests
{
    /// <summary>
    /// [TEST] GET /swagger/v1/swagger.json -> 200 OK + documento OpenAPI con los endpoints esperados.
    /// </summary>
    [Fact]
    public async Task Swagger_Endpoint_Returns_200_OK_With_Endpoints()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger/v1/swagger.json");

        // Assert: 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert: content-type es JSON
        Assert.Contains("application/json", response.Content.Headers.ContentType?.MediaType ?? "");

        // Assert: el body es JSON válido y contiene la estructura OpenAPI esperada
        var content = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(content);
        var root = doc.RootElement;

        // El documento debe tener "openapi" (versión del spec)
        Assert.True(root.TryGetProperty("openapi", out _), "El documento debe tener la propiedad 'openapi'");

        // El documento debe tener "info" con el título y descripción personalizados
        Assert.True(root.TryGetProperty("info", out var info), "El documento debe tener la propiedad 'info'");
        Assert.True(info.TryGetProperty("title", out var title), "'info' debe tener 'title'");
        Assert.Equal("Todo API", title.GetString());
        Assert.True(info.TryGetProperty("description", out var description), "'info' debe tener 'description'");
        Assert.Equal("API REST para gestionar tareas", description.GetString());
        Assert.True(info.TryGetProperty("version", out var version), "'info' debe tener 'version'");
        Assert.Equal("v1.0.0", version.GetString());

        // El documento debe tener "paths" con los endpoints de la API
        Assert.True(root.TryGetProperty("paths", out var paths), "El documento debe tener la propiedad 'paths'");

        // El controlador usa [Route("api/[controller]")] que genera /api/Tasks
        // y /api/Tasks/{id} con el nombre del controller (TasksController → Tasks)
        Assert.True(paths.TryGetProperty("/api/Tasks", out var tasksPath), "Debe existir /api/Tasks en paths");
        Assert.True(tasksPath.TryGetProperty("get", out _), "/api/Tasks debe tener GET");
        Assert.True(tasksPath.TryGetProperty("post", out _), "/api/Tasks debe tener POST");

        // GET /api/Tasks/{id} — obtener por ID
        // PUT /api/Tasks/{id} — actualizar
        // DELETE /api/Tasks/{id} — eliminar
        Assert.True(paths.TryGetProperty("/api/Tasks/{id}", out var taskByIdPath), "Debe existir /api/Tasks/{id} en paths");
        Assert.True(taskByIdPath.TryGetProperty("get", out _), "/api/Tasks/{id} debe tener GET");
        Assert.True(taskByIdPath.TryGetProperty("put", out _), "/api/Tasks/{id} debe tener PUT");
        Assert.True(taskByIdPath.TryGetProperty("delete", out _), "/api/Tasks/{id} debe tener DELETE");
    }

    /// <summary>
    /// [TEST] El documento OpenAPI debe contener los schemas de los DTOs
    /// en components/schemas con las propiedades correctas.
    /// </summary>
    [Fact]
    public async Task Swagger_Schemas_Include_All_DTOs()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger/v1/swagger.json");
        var content = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(content);
        var root = doc.RootElement;

        // Assert: debe tener components/schemas
        Assert.True(root.TryGetProperty("components", out var components),
            "El documento debe tener 'components'");
        Assert.True(components.TryGetProperty("schemas", out var schemas),
            "components debe tener 'schemas'");

        // ── TaskResponse ──────────────────────────────────────────────
        Assert.True(schemas.TryGetProperty("TaskResponse", out var taskResponse),
            "schemas debe contener 'TaskResponse'");
        Assert.True(taskResponse.TryGetProperty("type", out var trType));
        Assert.Equal("object", trType.GetString());

        Assert.True(taskResponse.TryGetProperty("properties", out var trProps));
        Assert.True(trProps.TryGetProperty("id", out var idProp));
        Assert.True(idProp.TryGetProperty("type", out var idType));
        Assert.Equal("string", idType.GetString());
        Assert.True(idProp.TryGetProperty("format", out var idFormat));
        Assert.Equal("uuid", idFormat.GetString());

        Assert.True(trProps.TryGetProperty("title", out var titleProp));
        Assert.True(titleProp.TryGetProperty("type", out var titleType));
        Assert.Equal("string", titleType.GetString());

        Assert.True(trProps.TryGetProperty("isCompleted", out var isCompletedProp));
        Assert.True(isCompletedProp.TryGetProperty("type", out var isCompletedType));
        Assert.Equal("boolean", isCompletedType.GetString());

        // ── CreateTaskRequest ─────────────────────────────────────────
        Assert.True(schemas.TryGetProperty("CreateTaskRequest", out var createReq),
            "schemas debe contener 'CreateTaskRequest'");
        Assert.True(createReq.TryGetProperty("type", out var crType));
        Assert.Equal("object", crType.GetString());

        Assert.True(createReq.TryGetProperty("properties", out var crProps));
        Assert.True(crProps.TryGetProperty("title", out var crTitle));
        Assert.True(crTitle.TryGetProperty("type", out var crTitleType));
        Assert.Equal("string", crTitleType.GetString());

        Assert.True(crProps.TryGetProperty("isCompleted", out var crCompleted));
        Assert.True(crCompleted.TryGetProperty("type", out var crCompletedType));
        Assert.Equal("boolean", crCompletedType.GetString());

        // ── UpdateTaskRequest ─────────────────────────────────────────
        Assert.True(schemas.TryGetProperty("UpdateTaskRequest", out var updateReq),
            "schemas debe contener 'UpdateTaskRequest'");
        Assert.True(updateReq.TryGetProperty("type", out var urType));
        Assert.Equal("object", urType.GetString());

        Assert.True(updateReq.TryGetProperty("properties", out var urProps));
        Assert.True(urProps.TryGetProperty("title", out var urTitle));
        Assert.True(urTitle.TryGetProperty("type", out var urTitleType));
        Assert.Equal("string", urTitleType.GetString());

        Assert.True(urProps.TryGetProperty("isCompleted", out var urCompleted));
        Assert.True(urCompleted.TryGetProperty("type", out var urCompletedType));
        Assert.Equal("boolean", urCompletedType.GetString());
    }
}
