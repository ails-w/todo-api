using System.ComponentModel.DataAnnotations;

namespace TodoApi.Api.Features.Tasks.Dtos;

/// <summary>
/// DTO de entrada para actualizar una tarea existente (PUT).
/// Misma validación que <see cref="CreateTaskRequest"/>.
/// </summary>
public record UpdateTaskRequest
{
    /// <summary>
    /// Título de la tarea. Obligatorio.
    /// </summary>
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 200 caracteres.")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Indica si la tarea está completada.
    /// </summary>
    public bool IsCompleted { get; init; }
}
