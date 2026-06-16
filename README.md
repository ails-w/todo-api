# Todo API

API REST para gestionar tareas, construida con ASP.NET Core como proyecto de aprendizaje progresivo.

## Estado actual

**Fase 9 — Cierre y documentación** 🏗️

Todas las fases de implementación están completas. API CRUD funcional con Swagger, error handling global, CI/CD, y 15/15 tests pasando.

## Features implementadas

| Feature | Endpoint | Estado |
|---|---|---|
| Listar tareas | `GET /api/tasks` | ✅ |
| Obtener tarea por ID | `GET /api/tasks/{id}` | ✅ |
| Crear tarea | `POST /api/tasks` | ✅ |
| Actualizar tarea | `PUT /api/tasks/{id}` | ✅ |
| Eliminar tarea | `DELETE /api/tasks/{id}` | ✅ |
| Documentación interactiva | `GET /swagger` | ✅ |
| Error handling global | Middleware `ExceptionMiddleware` | ✅ |
| CI/CD | GitHub Actions (build + lint + test) | ✅ |

## Stack

| Capa | Tecnología |
|---|---|
| Runtime | .NET 10 + ASP.NET Core |
| Tests | xUnit + WebApplicationFactory |
| API Docs | Swashbuckle.AspNetCore 10.x (Swagger/OpenAPI) |
| CI/CD | GitHub Actions |
| Linting | .editorconfig + analizadores Roslyn |

## Cómo empezar

```bash
# Clonar
git clone https://github.com/ails-w/todo-api.git
cd todo-api

# Build
dotnet build

# Ejecutar
dotnet run --project src/TodoApi.Api

# Abrir Swagger UI
# http://localhost:5158/swagger

# Tests
dotnet test
```

## Estructura del proyecto

```
TodoApi/
├── src/TodoApi.Api/           ← Aplicación Web API
│   ├── Controllers/           ← Punto de entrada HTTP
│   ├── Features/Tasks/        ← Feature Tasks (feature-first)
│   │   ├── Contracts/         ← Interfaces (ITaskRepository)
│   │   ├── Domain/            ← Modelo de dominio (TaskItem)
│   │   ├── Dtos/              ← DTOs de entrada/salida
│   │   └── Services/          ← Lógica de aplicación
│   ├── Infrastructure/        ← Implementaciones concretas
│   │   └── InMemory/          ← Repositorio en memoria
│   └── Middleware/             ← ExceptionMiddleware global
├── tests/TodoApi.Tests/       ← Tests de integración
└── docs/                      ← Documentación viva
    ├── architecture/          ← Decisiones de arquitectura
    ├── checklists/            ← Seguimiento de fases
    ├── handoffs/              ← Handoffs entre sesiones
    ├── learning/              ← Conceptos aprendidos por tema
    └── progress/              ← Bitácora de progreso por sesión
```

## API Endpoints

### `GET /api/tasks` — Listar todas las tareas

```json
// Response: 200 OK
[
  { "id": "guid", "title": "Comprar pan", "isCompleted": false }
]
```

### `GET /api/tasks/{id}` — Obtener tarea por ID
- `200 OK` + tarea si existe
- `404 Not Found` si no existe

### `POST /api/tasks` — Crear tarea

```json
// Request body:
{ "title": "Nueva tarea", "isCompleted": false }

// Response: 201 Created + Location: /api/tasks/{id}
```

### `PUT /api/tasks/{id}` — Actualizar tarea
- `200 OK` + tarea actualizada si existe
- `400 Bad Request` si el body es inválido
- `404 Not Found` si no existe

### `DELETE /api/tasks/{id}` — Eliminar tarea
- `204 NoContent` si se eliminó
- `404 Not Found` si no existe

## Documentación

- `docs/index.md` — Mapa central del proyecto
- `docs/project-state.md` — Estado actual y resumen final
- `docs/architecture/0001-architecture.md` — Decisiones de arquitectura
- `docs/learning/` — Conceptos aprendidos organizados por tema
- `docs/checklists/project-phases.md` — Seguimiento completo de fases

## Reglas de trabajo

- **TDD estricto**: test antes del código
- **Feature-first**: organización por funcionalidad, no por capas
- **Documentación viva**: cada avance deja rastro en `docs/progress/`
- **Commits convencionales**: `feat:`, `fix:`, `docs:`, etc.
