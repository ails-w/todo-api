# Estado del proyecto

## Estado actual

Fase: **7 — Swagger / OpenAPI (en progreso)**.  
Swagger configurado con Swashbuckle.AspNetCore 10.2.1. Tests de integración verifican endpoints, schemas y metadatos. 13/13 tests pasando.

## Qué ya está definido

- [x] el objetivo de aprendizaje
- [x] la estructura base del repositorio
- [x] el mapa central del proyecto
- [x] la estrategia de documentación para trabajo multi-chat
- [x] la decisión de arquitectura (API simple con feature-first)
- [x] los checklists operativos (start-session, implementation, review, before-merge, close-session)
- [x] el checklist general de fases del proyecto (reestructurado con tests primero)

## Qué ya está implementado (Fase 1)

- [x] solución .NET creada (`TodoApi.slnx`)
- [x] proyecto Web API creado (`src/TodoApi.Api/`)
- [x] `dotnet build` exitoso (0 errores, 0 warnings)
- [x] proyecto de tests xUnit creado con referencia a la API
- [x] paquete `Microsoft.AspNetCore.Mvc.Testing` instalado
- [x] `InternalsVisibleTo` configurado
- [x] test de health check escrito y pasando (`dotnet test` → 1/1 ✅)
- [x] runtime ASP.NET Core instalado en el entorno

## Qué ya está implementado (Fase 2)

- [x] modelo de dominio `TaskItem` (Id Guid, Title, IsCompleted)
- [x] interfaz `ITaskRepository` (GetAllAsync, GetByIdAsync)
- [x] `InMemoryTaskRepository` con datos semilla (3 tareas)
- [x] DTO de salida `TaskResponse` (record inmutable)
- [x] interfaz `ITaskService`
- [x] `TaskService` con GetAllAsync y GetByIdAsync + mapeo TaskItem → TaskResponse
- [x] `TasksController` con GET /api/tasks y GET /api/tasks/{id:guid}
- [x] DI registrada en Program.cs (Singleton repositorio, Scoped service)
- [x] endpoint `/weatherforecast` del template eliminado
- [x] tests de integración: 200 + lista, 200 por ID, 404 inexistente
- [x] `dotnet test` → 4/4 ✅

## Qué ya está implementado (Fase 4)

- [x] `UpdateTaskRequest` (DTO de entrada con `[Required]` y `[StringLength]`)
- [x] `UpdateAsync` en `ITaskRepository`
- [x] `UpdateAsync` en `InMemoryTaskRepository` (busca por ID, actualiza Title + IsCompleted)
- [x] `UpdateAsync` en `ITaskService` + `TaskService` (mapeo UpdateTaskRequest → TaskItem → TaskResponse)
- [x] `PUT /api/tasks/{id}` en `TasksController` (200 OK + tarea actualizada, 404 si no existe)
- [x] tests: PUT válido → 200, PUT inexistente → 404, PUT inválido → 400
- [x] `dotnet test` → 9/9 ✅

## Qué ya está implementado (Fase 3)

- [x] `CreateTaskRequest` (DTO de entrada con `[Required]` y `[StringLength]`)
- [x] `CreateAsync` en `ITaskRepository`
- [x] `CreateAsync` en `InMemoryTaskRepository` (asigna Guid automático)
- [x] `CreateAsync` en `ITaskService` + `TaskService` (mapeo CreateTaskRequest → TaskItem → TaskResponse)
- [x] `POST /api/tasks` en `TasksController` (201 Created + Location header)
- [x] tests: POST válido → 201, POST inválido → 400
- [x] documentación de conceptos: validation.md, http-methods.md actualizado
- [x] `dotnet test` → 6/6 ✅

## Qué ya está implementado (Fase 5)

- [x] `.editorconfig` con reglas de estilo .NET (indentación, naming, formateo, usings, análisis)
- [x] `Directory.Build.props` con `EnforceCodeStyleInBuild`, `EnableNETAnalyzers`, `AnalysisLevel`
- [x] `.github/workflows/ci.yml` con build + `dotnet format --verify-no-changes` en push (main/dev) y PR (main)
- [x] `.github/workflows/test.yml` con `dotnet test` en push (main/dev) y PR (main)
- [x] Workflows verificados en GitHub Actions (ambos success)
- [x] Pull Request #1 creada con plantilla del repositorio

## Qué ya está implementado (Fase 6)

- [x] `DeleteAsync` en `ITaskRepository` (`Task<bool>` — true si se eliminó, false si no existe)
- [x] `DeleteAsync` en `InMemoryTaskRepository` (busca, remueve, devuelve true/false)
- [x] `DeleteAsync` en `ITaskService` + `TaskService` (delega al repositorio)
- [x] `DELETE /api/tasks/{id}` en `TasksController` (204 NoContent / 404 NotFound)
- [x] Tests: DELETE existente → 204 + verificación de eliminación, DELETE inexistente → 404
- [x] `dotnet test` → 11/11 ✅

## Qué ya está implementado (Fase 7)

- [x] Paquete `Swashbuckle.AspNetCore` 10.2.1 instalado
- [x] Swagger configurado: `AddSwaggerGen`, `UseSwagger`, `UseSwaggerUI`
- [x] Título y descripción personalizados: "Todo API", "API REST para gestionar tareas", v1.0.0
- [x] `UseHttpsRedirection` condicional (solo en producción)
- [x] Tests: verifica 200 OK + endpoints listados + schemas de DTOs
- [x] `dotnet test` → 13/13 ✅

## Próximas fases

| Fase | Estado |
|---|---|
| 2 — GET /tasks | ✅ Completada |
| 3 — POST /tasks | ✅ Completada |
| 4 — PUT /tasks | ✅ Completada |
| 5 — CI/CD + Linting | ✅ Completada |
| 6 — DELETE /tasks | ✅ Completada |
| 7 — Swagger | ✅ Completada |
| 8 — Error handling | ⏳ Pendiente |
| 9 — Cierre | ⏳ Pendiente |

## Bloqueos

No hay bloqueos técnicos en este momento.

## Última sesión (2026-06-15, sesión 7)

- se instaló `Swashbuckle.AspNetCore` 10.2.1
- se configuró Swagger en Program.cs con título y descripción personalizados
- se desactivó `UseHttpsRedirection` en desarrollo
- se escribieron tests TDD para Swagger: endpoints listados y schemas de DTOs
- se probaron todos los endpoints manualmente desde Swagger UI
- se documentaron los conceptos en `docs/learning/swagger.md`
- `dotnet test` → 13/13 ✅

## Siguiente paso

Arrancar Fase 8 — Error handling global.
