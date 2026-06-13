# Handoff — Estado actual del proyecto

> Última actualización: 2026-06-13 — Sesión 6b

## Objetivo actual

✅ **Fase 6 completada** (DELETE /tasks/{id}).  
🎯 **Próximo objetivo: arrancar Fase 7 — Swagger / OpenAPI.**  
La Fase 6 incluyó: DELETE implementado con TDD (repositorio, servicio, controller), tests de integración, 11/11 tests pasando. PR #2 lista para crear.

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
- [x] **Fase 5: CI/CD real + Linting** — completa
  - `.editorconfig` con reglas de estilo .NET
  - `Directory.Build.props` con analizadores activados en build
  - Workflows reales: `ci.yml` (build + `dotnet format`) y `test.yml` (tests)
  - Verificación en GitHub Actions (ambos success)
  - PR #1 creada con cambios acumulados (Fases 2-5)
- [x] **Fase 6: DELETE /tasks/{id}** — completa
  - `DeleteAsync` en `ITaskRepository` + `InMemoryTaskRepository`
  - `DeleteAsync` en `ITaskService` + `TaskService`
  - `DELETE /api/tasks/{id}` en controller (204 NoContent / 404 NotFound)
  - Tests: DELETE existente → 204, DELETE inexistente → 404
  - `dotnet test` → 11/11 ✅
  - PR #2 lista con cambios acumulados (Fase 6)

### 🔄 En progreso

- Nada — Fase 6 completada, Fase 7 lista para arrancar

### ⏳ Próxima tarea

**Fase 7 — Swagger / OpenAPI.** Arrancar configuración de Swagger UI y personalización.

## Archivos que se deben leer primero

1. `README.md`
2. `docs/index.md`
3. `docs/project-state.md`
4. `docs/architecture/0001-architecture.md`
5. `docs/handoffs/chat-template.md` (este archivo)
6. `docs/checklists/project-phases.md` (Fase 6 — DELETE /tasks/{id})
7. `docs/learning/testing.md` (patrón de tests)
8. `docs/learning/http-methods.md` (DELETE semántica)

Luego leer los archivos específicos para la implementación (Fase 6):
- `src/TodoApi.Api/Controllers/TasksController.cs`
- `src/TodoApi.Api/Features/Tasks/Contracts/ITaskRepository.cs`
- `src/TodoApi.Api/Infrastructure/InMemory/InMemoryTaskRepository.cs`
- `src/TodoApi.Api/Features/Tasks/Services/ITaskService.cs`
- `src/TodoApi.Api/Features/Tasks/Services/TaskService.cs`
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
| `Task<bool>` para DeleteAsync (booleano, no null) | `ITaskRepository.cs` |
| DELETE → 204 NoContent, 404 si no existe | `TasksController.cs` |

## Qué NO debe hacer esta sesión

- NO tocar middleware de errores (Fase 8)
- NO instalar paquetes NuGet sin consultar
- NO borrar archivos de tests existentes sin confirmación
- NO modificar los workflows de CI/CD sin necesidad

## Reglas de trabajo

1. **TDD estricto**: escribir el test primero, después el código
2. **Explicar cada concepto** antes de escribir código
3. **Commits y PR**: cada fase incluye al menos 2 commits de avance propuestos por el asistente. Al completar implementación y documentación, el chat propondrá una Pull Request con los cambios acumulados
4. **Actualizar documentación** al cerrar la sesión:
   - `docs/progress/<fecha>.md`
   - `docs/project-state.md`
   - `docs/learning/` si aparece un concepto nuevo
   - `docs/checklists/project-phases.md` (marcar tareas completadas)
   - Este archivo (handoff)
5. Cada tarea de cada fase debe incluir su test correspondiente

## Cierre del chat

Antes de terminar, asegurarse de haber actualizado:
- [x] `docs/project-state.md`
- [x] `docs/progress/2026-06-13.md`
- [x] `docs/learning/http-methods.md` — DELETE semántica y ejemplo
- [x] `docs/checklists/project-phases.md` (Fase 6 marcada como completada)
- [x] Este archivo (handoff actualizado)
