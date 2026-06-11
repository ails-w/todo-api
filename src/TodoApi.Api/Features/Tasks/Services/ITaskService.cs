using TodoApi.Api.Features.Tasks.Dtos;

namespace TodoApi.Api.Features.Tasks.Services;

/// <summary>
/// Contrato del servicio de tareas, capa de lógica de aplicación.
/// Trabaja con DTOs, no con modelos de dominio.
/// </summary>
public interface ITaskService
{
    Task<List<TaskResponse>> GetAllAsync();
    Task<TaskResponse?> GetByIdAsync(Guid id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
}
