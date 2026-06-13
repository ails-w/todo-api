namespace TodoApi.Api.Features.Tasks.Dtos;

/// <summary>
/// DTO de salida que representa una tarea en la respuesta HTTP.
/// </summary>
/// <param name="Id">Identificador único de la tarea.</param>
/// <param name="Title">Título de la tarea.</param>
/// <param name="IsCompleted">Indica si la tarea está completada.</param>
public record TaskResponse(Guid Id, string Title, bool IsCompleted);
