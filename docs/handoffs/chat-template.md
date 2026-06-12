# Handoff — Estado actual del proyecto

> Última actualización: 2026-06-12 — Sesión 5

## Objetivo actual

Completar la **Fase 4** del proyecto: implementar **PUT /tasks/{id}**.  
✅ **Fase 4 completada.** Próximo objetivo: arrancar **Fase 5 — DELETE /tasks/{id}**.

## Estado actual

### ✅ Completado hasta ahora

- [x] **Fase 0**: documentación base, arquitectura, checklists
- [x] **Fase 1**: scaffolding completo (solución, proyecto API, tests, build, health check)
- [x] **Fase 2: GET /tasks** — completa
  - Modelo `TaskItem`, `ITaskRepository`, `InMemoryTaskRepository`
  - DTO `TaskResponse`, `ITaskService` + `TaskService`
  - `TasksController` con GET /api/tasks y GET /api/tasks/{id:guid}
  - DI registrada, tests de integración, 4/4 ✅
- [x] **Fase 3: POST /tasks** — completa
  - `CreateTaskRequest` con `[Required]` y `[StringLength]`
  - `CreateAsync` en repositorio, servicio y controller
  - `POST /api/tasks` con 201 Created + Location header
  - Tests: POST válido → 201, POST inválido → 400
  - `dotnet test` → 6/6 ✅
  - Checklist reestructurado: tests primero (TDD)
- [x] **Fase 4: PUT /tasks/{id}** — completa
  - `UpdateTaskRequest` con `[Required]` y `[StringLength]`
  - `UpdateAsync` en repositorio, servicio y controller
  - `PUT /api/tasks/{id}` con 200 OK, 404 si no existe
  - Tests: PUT válido → 200, PUT inexistente → 404, PUT inválido → 400
  - `dotnet test` → 9/9 ✅

### 🔄 En progreso

- Nada — Fase 4 completada, Fase 5 lista para arrancar

### ⏳ Próxima tarea

**Fase 5 — DELETE /tasks/{id}.** Empezar por:

1. **[TEST]** DELETE existente → 204 NoContent (escribir test → falla)
2. **[TEST]** DELETE inexistente → 404 (escribir test → falla)

El orden de las tareas está en `docs/checklists/project-phases.md` (Fase 5).

## Archivos que se deben leer primero

1. `README.md`
2. `docs/index.md`
3. `docs/project-state.md`
4. `docs/architecture/0001-architecture.md`
5. `docs/handoffs/chat-template.md` (este archivo)
6. `docs/checklists/project-phases.md` (Fase 5)
7. `docs/learning/testing.md` (patrón de tests)
8. `docs/learning/validation.md` (Data Annotations)

Luego leer los archivos específicos para la implementación:
- `src/TodoApi.Api/Program.cs`
- `src/TodoApi.Api/Controllers/TasksController.cs`
- `src/TodoApi.Api/Features/Tasks/Contracts/ITaskRepository.cs`
- `src/TodoApi.Api/Infrastructure/InMemory/InMemoryTaskRepository.cs`
- `src/TodoApi.Api/Features/Tasks/Services/ITaskService.cs`
- `src/TodoApi.Api/Features/Tasks/Services/TaskService.cs`
- `src/TodoApi.Api/Features/Tasks/Dtos/UpdateTaskRequest.cs`
- `tests/TodoApi.Tests/TasksControllerTests.cs`

## Decisiones ya tomadas

| Decisión | Referencia |
|---|---|
| API simple con feature-first | `docs/architecture/0001-architecture.md` |
| Almacenamiento en memoria (InMemory) | `docs/architecture/0001-architecture.md` |
| TDD estricto: test antes del código | Regla de trabajo |
| Tests de integración con WebApplicationFactory | `docs/learning/testing.md` |
| Formato .slnx (default de .NET 10) | — |
| Rama `dev` para desarrollo, PRs a `main` | — |
| Guid como Id del modelo de dominio | `TaskItem.cs` |
| DTOs como records inmutables (`init`) | `TaskResponse.cs`, `CreateTaskRequest.cs` |
| Validación declarativa con Data Annotations | `CreateTaskRequest.cs` |
| Singleton para repositorio en memoria | `Program.cs` |
| Scoped para servicios de aplicación | `Program.cs` |
| `CreatedAtAction` para 201 + Location | `TasksController.cs` |
| PUT con 200 OK + body (no 204) | `TasksController.cs` |
| `[FromBody]` explícito en endpoints con ruta + body | `TasksController.cs` |

## Qué NO debe hacer esta sesión

- NO implementar DELETE sin TDD (es Fase 5, ya con tests)
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
