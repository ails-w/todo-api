using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoApi.Api.Features.Tasks.Dtos;

namespace TodoApi.Tests;

/// <summary>
/// Tests de integración para el controller TasksController.
/// Verifican el comportamiento completo: HTTP → routing → controller → service → repository.
/// </summary>
public class TasksControllerTests
{
    /// <summary>
    /// GET /api/tasks → 200 OK + lista JSON no vacía.
    /// Verifica que el endpoint devuelve las tareas semilla del repositorio.
    /// </summary>
    [Fact]
    public async Task Get_AllTasks_Returns_200_OK_With_List()
    {
        // Arrange: levantar la API en memoria
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act: disparar GET /api/tasks
        var response = await client.GetAsync("/api/tasks");

        // Assert: 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert: el body se deserializa como List<TaskResponse> y no está vacío
        var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponse>>();
        Assert.NotNull(tasks);
        Assert.NotEmpty(tasks);
    }

    /// <summary>
    /// GET /api/tasks/{id} con ID existente → 200 OK + tarea con ese ID.
    /// Primero obtiene todas las tareas para conseguir un ID real,
    /// luego pide esa tarea por ID.
    /// </summary>
    [Fact]
    public async Task Get_TaskById_Existing_Returns_200_OK_With_Task()
    {
        // Arrange: conseguir un ID que existe en las tareas semilla
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var allResponse = await client.GetAsync("/api/tasks");
        var allTasks = await allResponse.Content.ReadFromJsonAsync<List<TaskResponse>>();
        var existingId = allTasks!.First().Id;

        // Act: GET /api/tasks/{existingId}
        var response = await client.GetAsync($"/api/tasks/{existingId}");

        // Assert: 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert: la tarea devuelta tiene el ID que pedimos
        var task = await response.Content.ReadFromJsonAsync<TaskResponse>();
        Assert.NotNull(task);
        Assert.Equal(existingId, task.Id);
    }

    /// <summary>
    /// GET /api/tasks/{id} con ID inexistente → 404 NotFound.
    /// Usa un Guid aleatorio que no puede existir en el repositorio.
    /// </summary>
    [Fact]
    public async Task Get_TaskById_NonExisting_Returns_404_NotFound()
    {
        // Arrange: ID que no existe
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var nonExistingId = Guid.NewGuid();

        // Act: GET /api/tasks/{nonExistingId}
        var response = await client.GetAsync($"/api/tasks/{nonExistingId}");

        // Assert: 404
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
