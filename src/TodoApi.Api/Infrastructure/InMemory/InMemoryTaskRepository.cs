using TodoApi.Api.Features.Tasks.Contracts;
using TodoApi.Api.Features.Tasks.Domain;

namespace TodoApi.Api.Infrastructure.InMemory;

/// <summary>
/// Repositorio en memoria que implementa <see cref="ITaskRepository"/>.
/// Almacena las tareas en una lista y las inicializa con datos semilla.
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks =
    [
        new() { Id = Guid.NewGuid(), Title = "Aprender ASP.NET Core", IsCompleted = false },
        new() { Id = Guid.NewGuid(), Title = "Crear endpoints GET", IsCompleted = false },
        new() { Id = Guid.NewGuid(), Title = "Escribir tests", IsCompleted = true },
    ];

    public Task<List<TaskItem>> GetAllAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<TaskItem?> GetByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.Id = Guid.NewGuid();
        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task<TaskItem?> UpdateAsync(Guid id, TaskItem task)
    {
        var existing = _tasks.FirstOrDefault(t => t.Id == id);
        if (existing is null)
            return Task.FromResult<TaskItem?>(null);

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;
        return Task.FromResult<TaskItem?>(existing);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return Task.FromResult(false);

        _tasks.Remove(task);
        return Task.FromResult(true);
    }
}
