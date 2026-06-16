# Mapa central del proyecto Todo API

Este documento es la **fuente de verdad** para navegar el repositorio.  
Si alguien entra al proyecto por primera vez, debe leer esto antes de tocar código o documentación.

## Objetivo del proyecto

API REST para gestionar tareas, construida con ASP.NET Core como proyecto de aprendizaje progresivo.

## Arquitectura

### Decisión base

API simple con organización **feature-first** y almacenamiento en memoria.

| Aspecto | Elección | Motivo |
|---|---|---|
| Organización | Feature-first | Equilibrio entre orden y simplicidad. Cada funcionalidad agrupa su modelo, contratos, DTOs y servicios. |
| Almacenamiento | En memoria (InMemory) | Aprendizaje. Sin dependencias externas, sin setup de base de datos. |
| Controllers | Tradicionales (con atributos) | Claridad de routing vs Minimal APIs. |
| Validación | Data Annotations | Declarativa, automática con `[ApiController]`. |
| DTOs | Records inmutables | Separación dominio/contrato público. |
| Tests | Integración con WebApplicationFactory | Prueban la API completa sin abrir puertos reales. |

### Alternativas consideradas

1. **Clean Architecture completa** — buena separación pero agrega complejidad innecesaria para el alcance actual.
2. **Monolito plano con lógica en controllers** — enseña malos hábitos y mezcla responsabilidades.
3. **Minimal API** — más sintética pero menos organizada para proyectos con múltiples endpoints.

### Tradeoffs

Elegimos claridad y aprendizaje por encima de abstracción prematura.  
La estructura es suficientemente ordenada para crecer después, sin ocultar los fundamentos.

## Mapa de carpetas

| Ruta | Propósito |
|---|---|
| `src/TodoApi.Api/` | Proyecto principal de la API |
| `tests/TodoApi.Tests/` | Pruebas automatizadas |
| `docs/` | Documentación viva del proyecto |
| `.github/` | Workflows y plantillas para GitHub |

### `src/TodoApi.Api/`

| Carpeta/Archivo | Propósito |
|---|---|
| `Controllers/` | Endpoints HTTP (TasksController) |
| `Features/Tasks/` | Feature Tasks (feature-first) |
| `Features/Tasks/Contracts/` | Interfaces (ITaskRepository) |
| `Features/Tasks/Domain/` | Modelo de dominio (TaskItem) |
| `Features/Tasks/Dtos/` | DTOs de entrada/salida |
| `Features/Tasks/Services/` | Lógica de aplicación |
| `Infrastructure/InMemory/` | Repositorio en memoria |
| `Middleware/` | Middleware global (ExceptionMiddleware) |
| `Program.cs` | Punto de entrada, DI, pipeline |

### `tests/TodoApi.Tests/`

| Archivo | Propósito |
|---|---|
| `TasksControllerTests.cs` | Tests CRUD de endpoints |
| `SwaggerEndpointTests.cs` | Tests de documento OpenAPI |
| `ErrorHandlingTests.cs` | Tests de manejo global de errores |
| `ApiHealthCheckTests.cs` | Health check inicial |

### `docs/`

| Archivo/Carpeta | Propósito |
|---|---|
| `index.md` | ⬅️ Este archivo, mapa central del proyecto |
| `project-state.md` | Estado actual y resumen final |
| `handoffs/chat-template.md` | Handoff para continuidad entre chats |
| `checklists/project-phases.md` | Seguimiento de fases del proyecto |
| `learning/` | Conceptos aprendidos por tema |
| `progress/` | Bitácora cronológica de sesiones |
| `_template/` | Plantilla genérica de estructura (no forma parte del proyecto) |

### `.github/`

| Archivo | Propósito |
|---|---|
| `workflows/ci.yml` | Build + lint en push y PR |
| `workflows/test.yml` | Tests automatizados en push y PR |
| `pull_request_template.md` | Plantilla para Pull Requests |

## Documentos importantes

| Documento | Para qué sirve |
|---|---|
| `README.md` | Entrada rápida al proyecto |
| `docs/index.md` | Mapa maestro del repositorio |
| `docs/project-state.md` | Estado actual y siguiente paso |
| `docs/handoffs/chat-template.md` | Continuidad entre chats |
| `docs/learning/*.md` | Aprendizaje por concepto |
| `docs/progress/*.md` | Registro de sesiones |
| `docs/checklists/project-phases.md` | Seguimiento de fases |

## Flujo de trabajo recomendado

1. Leer `README.md`
2. Leer este archivo (`docs/index.md`)
3. Leer `docs/project-state.md`
4. Leer el último archivo de `docs/progress/`
5. Leer `docs/handoffs/chat-template.md`
6. Trabajar solo sobre la tarea actual
7. Actualizar estado, progreso y aprendizaje al cerrar

## Regla de mantenimiento

Si una carpeta cambia de propósito o se agrega una decisión importante, se debe actualizar:

- el documento que corresponda en `docs/`
- el estado del proyecto
- el registro de progreso
