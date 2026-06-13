using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;

using TodoApi.Api.Features.Tasks.Dtos;

namespace TodoApi.Tests;

/// <summary>
/// Tests de integración para el controller TasksController.
/// Verifican el comportamiento completo: HTTP -> routing -> controller -> service -> repository.
/// Ordenados por flujo TDD: primero los tests del feature nuevo (POST), luego los existentes (GET).
/// </summary>
public class TasksControllerTests
{
    // ======================================================================
    // POST /api/tasks — tests escritos primero (TDD)
    // ======================================================================

    /// <summary>
    /// [TEST] POST válido -> 201 Created + Location header + tarea creada.
    /// </summary>
    [Fact]
    public async Task Post_Task_Valid_Returns_201_Created_With_Location()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var request = new CreateTaskRequest { Title = "Nueva tarea de test", IsCompleted = false };

        // Act
        var response = await client.PostAsJsonAsync("/api/tasks", request);

        // Assert: 201 Created
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        // Assert: Location header presente con la URL de la tarea creada
        Assert.NotNull(response.Headers.Location);
        var locationPath = response.Headers.Location.ToString();
        Assert.Contains("/api/tasks/", locationPath, StringComparison.OrdinalIgnoreCase);

        // Assert: body contiene la tarea creada con ID asignado
        var createdTask = await response.Content.ReadFromJsonAsync<TaskResponse>();
        Assert.NotNull(createdTask);
        Assert.Equal(request.Title, createdTask.Title);
        Assert.Equal(request.IsCompleted, createdTask.IsCompleted);
        Assert.NotEqual(Guid.Empty, createdTask.Id);

        // Assert: realmente se guardó (GET por ID devuelve la tarea)
        var getResponse = await client.GetAsync(locationPath);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    /// <summary>
    /// [TEST] POST inválido (Title vacío) -> 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task Post_Task_Invalid_Returns_400_BadRequest()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var request = new CreateTaskRequest { Title = "", IsCompleted = false };

        // Act
        var response = await client.PostAsJsonAsync("/api/tasks", request);

        // Assert: 400 Bad Request
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ======================================================================
    // PUT /api/tasks/{id} — tests escritos primero (TDD, Fase 4)
    // ======================================================================

    /// <summary>
    /// [TEST] PUT válido -> 200 OK + tarea actualizada con los nuevos valores.
    /// </summary>
    [Fact]
    public async Task Put_Task_Valid_Returns_200_OK_With_UpdatedTask()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Obtener una tarea existente de los datos semilla
        var allResponse = await client.GetAsync("/api/tasks");
        var allTasks = await allResponse.Content.ReadFromJsonAsync<List<TaskResponse>>();
        var existingTask = allTasks!.First();

        var updateRequest = new UpdateTaskRequest
        {
            Title = "Título actualizado por PUT",
            IsCompleted = true
        };

        // Act
        var response = await client.PutAsJsonAsync($"/api/tasks/{existingTask.Id}", updateRequest);

        // Assert: 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert: body contiene la tarea actualizada
        var updatedTask = await response.Content.ReadFromJsonAsync<TaskResponse>();
        Assert.NotNull(updatedTask);
        Assert.Equal(updateRequest.Title, updatedTask.Title);
        Assert.Equal(updateRequest.IsCompleted, updatedTask.IsCompleted);
        Assert.Equal(existingTask.Id, updatedTask.Id);

        // Assert: realmente se persistió (GET por ID devuelve los valores actualizados)
        var getResponse = await client.GetAsync($"/api/tasks/{existingTask.Id}");
        var verifiedTask = await getResponse.Content.ReadFromJsonAsync<TaskResponse>();
        Assert.NotNull(verifiedTask);
        Assert.Equal(updateRequest.Title, verifiedTask.Title);
        Assert.Equal(updateRequest.IsCompleted, verifiedTask.IsCompleted);
    }

    /// <summary>
    /// [TEST] PUT con ID inexistente -> 404 NotFound.
    /// </summary>
    [Fact]
    public async Task Put_Task_NonExisting_Returns_404_NotFound()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var nonExistingId = Guid.NewGuid();
        var updateRequest = new UpdateTaskRequest
        {
            Title = "No importa",
            IsCompleted = false
        };

        // Act
        var response = await client.PutAsJsonAsync($"/api/tasks/{nonExistingId}", updateRequest);

        // Assert: 404 Not Found
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// [TEST] PUT inválido (Title vacío) -> 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task Put_Task_Invalid_Returns_400_BadRequest()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var updateRequest = new UpdateTaskRequest
        {
            Title = "", // inválido: [Required] + [StringLength(MinimumLength = 1)]
            IsCompleted = false
        };

        // Act
        var response = await client.PutAsJsonAsync($"/api/tasks/{Guid.NewGuid()}", updateRequest);

        // Assert: 400 Bad Request
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ======================================================================
    // DELETE /api/tasks/{id} — tests escritos primero (TDD, Fase 6)
    // ======================================================================

    /// <summary>
    /// [TEST] DELETE existente -> 204 NoContent + recurso eliminado.
    /// </summary>
    [Fact]
    public async Task Delete_Task_Existing_Returns_204_NoContent()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Obtener una tarea existente de los datos semilla
        var allResponse = await client.GetAsync("/api/tasks");
        var allTasks = await allResponse.Content.ReadFromJsonAsync<List<TaskResponse>>();
        var existingTask = allTasks!.First();

        // Act
        var response = await client.DeleteAsync($"/api/tasks/{existingTask.Id}");

        // Assert: 204 No Content
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Assert: realmente se eliminó (GET por ID devuelve 404)
        var getResponse = await client.GetAsync($"/api/tasks/{existingTask.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    /// <summary>
    /// [TEST] DELETE con ID inexistente -> 404 NotFound.
    /// </summary>
    [Fact]
    public async Task Delete_Task_NonExisting_Returns_404_NotFound()
    {
        // Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var nonExistingId = Guid.NewGuid();

        // Act
        var response = await client.DeleteAsync($"/api/tasks/{nonExistingId}");

        // Assert: 404 Not Found
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ======================================================================
    // GET /api/tasks — tests heredados de Fase 2
    // ======================================================================

    /// <summary>
    /// GET /api/tasks -> 200 OK + lista JSON no vacía.
    /// </summary>
    [Fact]
    public async Task Get_AllTasks_Returns_200_OK_With_List()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/tasks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponse>>();
        Assert.NotNull(tasks);
        Assert.NotEmpty(tasks);
    }

    /// <summary>
    /// GET /api/tasks/{id} con ID existente -> 200 OK + tarea con ese ID.
    /// </summary>
    [Fact]
    public async Task Get_TaskById_Existing_Returns_200_OK_With_Task()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var allResponse = await client.GetAsync("/api/tasks");
        var allTasks = await allResponse.Content.ReadFromJsonAsync<List<TaskResponse>>();
        var existingId = allTasks!.First().Id;

        var response = await client.GetAsync($"/api/tasks/{existingId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var task = await response.Content.ReadFromJsonAsync<TaskResponse>();
        Assert.NotNull(task);
        Assert.Equal(existingId, task.Id);
    }

    /// <summary>
    /// GET /api/tasks/{id} con ID inexistente -> 404 NotFound.
    /// </summary>
    [Fact]
    public async Task Get_TaskById_NonExisting_Returns_404_NotFound()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var nonExistingId = Guid.NewGuid();

        var response = await client.GetAsync($"/api/tasks/{nonExistingId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
