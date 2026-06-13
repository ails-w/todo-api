namespace TodoApi.Api.Features.Tasks.Domain;

/// <summary>
/// Modelo de dominio que representa una tarea.
/// </summary>
public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
