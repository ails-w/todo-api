# Mapa central del proyecto Todo API

Este documento es la **fuente de verdad** para navegar el repositorio.  
Si alguien entra al proyecto por primera vez, debe leer esto antes de tocar código o documentación.

## Objetivo del proyecto

Aprender ASP.NET Core básico construyendo una API de tareas con:

- Web API
- Swagger
- almacenamiento en memoria
- Controllers
- Routing
- HTTP methods
- JSON
- Dependency Injection
- DTOs

## Mapa de carpetas

| Ruta | Propósito |
|---|---|
| `src/` | Código de la aplicación |
| `src/TodoApi.Api/` | Proyecto principal de la API cuando se implemente |
| `tests/` | Pruebas automatizadas |
| `docs/` | Documentación viva del proyecto |
| `.github/` | Workflows y plantillas para GitHub |

## Qué debe haber en cada área

### `src/TodoApi.Api/`
- `Controllers/` → endpoints HTTP
- `Features/Tasks/` → funcionalidad de tareas
- `Infrastructure/InMemory/` → almacenamiento temporal
- `Middleware/` → middlewares globales (ExceptionMiddleware)
- `Extensions/` → configuración y extensiones

### `tests/TodoApi.Tests/`
- pruebas unitarias
- pruebas de integración
- validaciones de comportamiento

### `docs/`
- `project-state.md` → estado actual del proyecto
- `architecture/0001-architecture.md` → decisión base de arquitectura
- `learning/` → conceptos y aprendizaje por tema
- `progress/` → registro cronológico del avance
- `handoffs/chat-template.md` → plantilla para continuar en otro chat
- `checklists/` → listas de verificación operativas
- `adr/` → decisiones de arquitectura

### `.github/`
- `workflows/ci.yml` → base del flujo de verificación
- `workflows/test.yml` → flujo base para pruebas automatizadas
- `pull_request_template.md` → plantilla para PRs

## Documentos importantes

| Documento | Para qué sirve |
|---|---|
| `README.md` | Entrada rápida al proyecto |
| `docs/index.md` | Mapa maestro del repositorio |
| `docs/project-state.md` | Estado actual y próximo paso |
| `docs/architecture/0001-architecture.md` | Arquitectura elegida y razones |
| `docs/handoffs/chat-template.md` | Continuidad entre chats |
| `docs/learning/*.md` | Aprendizaje por concepto |
| `docs/progress/*.md` | Registro de sesiones |
| `docs/checklists/*.md` | Guías operativas |
| `docs/adr/*.md` | Decisiones que no deben reabrirse sin motivo |

## Flujo recomendado para trabajar con otro agente

1. Leer `README.md`
2. Leer este archivo (`docs/index.md`)
3. Leer `docs/project-state.md`
4. Leer el último archivo de `docs/progress/`
5. Leer `docs/architecture/0001-architecture.md`
6. Leer `docs/handoffs/chat-template.md`
7. Revisar `docs/checklists/start-session.md`
8. Trabajar solo sobre la tarea actual
9. Actualizar estado, progreso y aprendizaje al cerrar

## Orden sugerido de lectura

1. `README.md`
2. `docs/index.md`
3. `docs/project-state.md`
4. `docs/architecture/0001-architecture.md`
5. `docs/handoffs/chat-template.md`
6. `docs/checklists/start-session.md`

## Regla de mantenimiento

Si una carpeta cambia de propósito o se agrega una decisión importante, se debe actualizar:

- el documento que corresponda en `docs/`
- el estado del proyecto
- el registro de progreso
