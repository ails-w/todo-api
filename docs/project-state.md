# Estado del proyecto

## Estado actual

Fase: **3 — POST /tasks (completada)**.  
DTO de entrada, validación con Data Annotations, repositorio, servicio, controller y tests de integración funcionando. 6/6 tests pasando.

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
|---|---|
| 2 — GET /tasks | ✅ Completada |
| 3 — POST /tasks | ✅ Completada |
| 4 — PUT /tasks | Pendiente |
| 5 — DELETE /tasks | Pendiente |
| 6 — Swagger | Pendiente |
| 7 — Error handling | Pendiente |
| 8 — CI/CD | Pendiente |
| 9 — Cierre | Pendiente |

## Bloqueos

No hay bloqueos técnicos en este momento.

## Última sesión (2026-06-11, sesión 4)

- se implementó la Fase 3 completa: CreateTaskRequest, validación, CreateAsync en repositorio/servicio/controller
- se escribieron los tests primero (TDD estricto)
- se reestructuró el checklist de fases (tests primero)
- se documentaron Data Annotations en `docs/learning/validation.md`
- se actualizó `docs/learning/http-methods.md` con 201 Created y POST
- `dotnet build` y `dotnet test` pasan (6/6 ✅)

## Siguiente paso

Arrancar Fase 4 (PUT /tasks/{id}).
