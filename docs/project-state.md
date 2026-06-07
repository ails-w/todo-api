# Estado del proyecto

## Estado actual

Fase: **2 — GET /tasks (completada)**.  
Modelo de dominio, repositorio, servicio, controller y tests de integración funcionando. 4/4 tests pasando.

## Qué ya está definido

- [x] el objetivo de aprendizaje
- [x] la estructura base del repositorio
- [x] el mapa central del proyecto
- [x] la estrategia de documentación para trabajo multi-chat
- [x] la decisión de arquitectura (API simple con feature-first)
- [x] los checklists operativos (start-session, implementation, review, before-merge, close-session)
- [x] el checklist general de fases del proyecto

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

## Próximas fases

| Fase | Estado |
|---|---|
| 2 — GET /tasks | ✅ Completada |
| 3 — POST /tasks | Pendiente |
| 4 — PUT /tasks | Pendiente |
| 5 — DELETE /tasks | Pendiente |
| 6 — Swagger | Pendiente |
| 7 — Error handling | Pendiente |
| 8 — CI/CD | Pendiente |
| 9 — Cierre | Pendiente |

## Bloqueos

No hay bloqueos técnicos en este momento.

## Última sesión (2026-06-07, sesión 3)

- se implementó la Fase 2 completa: modelo, repositorio, servicio, controller, DI
- se eliminó el endpoint `/weatherforecast` del template original
- se actualizó el health check a GET /api/tasks
- se crearon 3 tests de integración para los endpoints GET
- `dotnet build` y `dotnet test` pasan (4/4 ✅)

## Siguiente paso

Arrancar Fase 3 (POST /tasks).
