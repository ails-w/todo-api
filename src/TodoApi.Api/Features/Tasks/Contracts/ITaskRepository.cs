using TodoApi.Api.Features.Tasks.Domain;

namespace TodoApi.Api.Features.Tasks.Contracts;

/// <summary>
/// Contrato que define las operaciones de almacenamiento de tareas.
/// </summary>
public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(Guid id, TaskItem task);
    Task<bool> DeleteAsync(Guid id);
}
