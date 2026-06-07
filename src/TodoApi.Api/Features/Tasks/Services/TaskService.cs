using TodoApi.Api.Features.Tasks.Contracts;
using TodoApi.Api.Features.Tasks.Dtos;

namespace TodoApi.Api.Features.Tasks.Services;

/// <summary>
/// Implementa <see cref="ITaskService"/> usando <see cref="ITaskRepository"/>.
/// Orquesta: repositorio → mapeo a DTO → respuesta.
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskResponse>> GetAllAsync()
    {
        var tasks = await _repository.GetAllAsync();

        return tasks.Select(t => new TaskResponse(t.Id, t.Title, t.IsCompleted)).ToList();
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        return task is null
            ? null
            : new TaskResponse(task.Id, task.Title, task.IsCompleted);
    }
}
