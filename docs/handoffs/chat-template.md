# Handoff — Estado actual del proyecto

> Última actualización: 2026-06-07 — Sesión 3

## Objetivo actual

Completar la **Fase 2** del proyecto: implementar el feature Tasks con endpoints GET.  
✅ **Fase 2 completada.** Próximo objetivo: arrancar **Fase 3 — POST /tasks**.

## Estado actual

### ✅ Completado hasta ahora

- [x] Fase 0: documentación base, arquitectura, checklists
- [x] Fase 1: scaffolding completo
  - Solución .NET (`TodoApi.slnx`) con proyecto API y de tests
  - `dotnet build` y `dotnet test` pasando
  - Runtime ASP.NET Core instalado en Arch Linux
  - Conceptos documentados en `docs/learning/`
- [x] **Fase 2: GET /tasks** — completa
  - Modelo `TaskItem` (`Features/Tasks/Domain/`)
  - Interfaz `ITaskRepository` (`Features/Tasks/Contracts/`)
  - `InMemoryTaskRepository` con 3 tareas semilla (`Infrastructure/InMemory/`)
  - DTO `TaskResponse` como record (`Features/Tasks/Dtos/`)
  - Interfaz `ITaskService` y `TaskService` con GetAllAsync + GetByIdAsync (`Features/Tasks/Services/`)
  - `TasksController` con GET /api/tasks y GET /api/tasks/{id:guid} (`Controllers/`)
  - DI registrada en Program.cs
  - Tests de integración: 200 + lista, 200 por ID, 404 inexistente
  - `dotnet test` → 4/4 ✅

### 🔄 En progreso

- Nada — Fase 2 completada, Fase 3 lista para arrancar

### ⏳ Próxima tarea

**Fase 3 — POST /tasks.** Empezar por:

1. Tarea 3.1: Crear `CreateTaskRequest` (DTO de entrada con validación)
2. Tarea 3.2: Agregar validación con anotaciones (`[Required]`, etc.)
3. Seguir secuencialmente hasta tarea 3.10

El orden de las tareas está en `docs/checklists/project-phases.md` (Fase 3).

## Archivos que se deben leer primero

1. `README.md`
2. `docs/index.md`
3. `docs/project-state.md`
4. `docs/architecture/0001-architecture.md`
5. `docs/handoffs/chat-template.md` (este archivo)
6. `docs/checklists/project-phases.md` (para ver la lista de tareas de Fase 3)
7. `docs/learning/testing.md` (patrón de tests)

Luego leer los archivos específicos para la implementación:
- `src/TodoApi.Api/Program.cs`
- `src/TodoApi.Api/Controllers/TasksController.cs`
- `src/TodoApi.Api/Features/Tasks/Services/ITaskService.cs`
- `src/TodoApi.Api/Features/Tasks/Services/TaskService.cs`
- `src/TodoApi.Api/Infrastructure/InMemory/InMemoryTaskRepository.cs`
- `tests/TodoApi.Tests/TasksControllerTests.cs`

## Decisiones ya tomadas

| Decisión | Referencia |
|---|---|
| API simple con feature-first | `docs/architecture/0001-architecture.md` |
| Almacenamiento en memoria (InMemory) | `docs/architecture/0001-architecture.md` |
| TDD estricto: escribir test antes del código | Regla de trabajo |
| Tests de integración con WebApplicationFactory | `docs/learning/testing.md` |
| Formato .slnx (default de .NET 10) | — |
| Rama `dev` para desarrollo, PRs a `main` | — |
| Guid como Id del modelo de dominio | `TaskItem.cs` |
| DTOs como records inmutables | `TaskResponse.cs` |
| Singleton para repositorio en memoria | `Program.cs` |
| Scoped para servicios de aplicación | `Program.cs` |

## Qué NO debe hacer esta sesión

- NO implementar PUT, DELETE (son Fase 4, 5)
- NO tocar middleware de errores (Fase 7)
- NO modificar workflows de CI/CD (Fase 8)
- NO instalar paquetes NuGet sin consultar
- NO borrar archivos de tests existentes sin confirmación

## Reglas de trabajo

1. **TDD estricto**: escribir el test primero, después el código
2. **Explicar cada concepto** antes de escribir código
3. **Actualizar documentación** al cerrar la sesión:
   - `docs/progress/<fecha>.md`
   - `docs/project-state.md`
   - `docs/learning/` si aparece un concepto nuevo
   - `docs/checklists/project-phases.md` (marcar tareas completadas)
   - Este archivo (handoff)
4. Cada tarea de cada fase debe incluir su test correspondiente

## Cierre del chat

Antes de terminar, asegurarse de haber actualizado:
- [ ] `docs/project-state.md`
- [ ] `docs/progress/<fecha>.md`
- [ ] `docs/learning/` si apareció un concepto nuevo
- [ ] `docs/checklists/project-phases.md` (tareas completadas marcadas)
- [ ] Este archivo (handoff actualizado)
