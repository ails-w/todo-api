# Estado del proyecto

## Estado actual

Fase: **4 — PUT /tasks/{id} (completada)**.  
DTO de entrada, validación, UpdateAsync en repositorio/servicio/controller, tests de integración funcionando. 9/9 tests pasando.

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

## Próximas fases

| Fase | Estado |
|---|---|---|
| 2 — GET /tasks | ✅ Completada |
| 3 — POST /tasks | ✅ Completada |
| 4 — PUT /tasks | ✅ Completada |
| 5 — DELETE /tasks | Pendiente |
| 6 — Swagger | Pendiente |
| 7 — Error handling | Pendiente |
| 8 — CI/CD | Pendiente |
| 9 — Cierre | Pendiente |

## Bloqueos

No hay bloqueos técnicos en este momento.

## Última sesión (2026-06-12, sesión 5)

- se implementó la Fase 4 completa: UpdateTaskRequest, validación, UpdateAsync en repositorio/servicio/controller
- se escribieron los tests primero (TDD estricto)
- PUT válido → 200, PUT inexistente → 404, PUT inválido → 400
- `dotnet build` y `dotnet test` pasan (9/9 ✅)

## Siguiente paso

Arrancar Fase 5 (DELETE /tasks/{id}).
