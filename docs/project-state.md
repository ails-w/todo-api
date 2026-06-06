# Estado del proyecto

## Estado actual

Fase: **1 — Scaffolding (en progreso)**.  
Solución y proyecto API creados, build exitoso, test de health check pasando.

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

## Qué falta (Fase 1 — documentación)

- [x] documentar conceptos aprendidos en `docs/learning/`
- [x] revisar el código creado y entender su funcionamiento

## Próximas fases

| Fase | Estado |
|---|---|
| 2 — GET /tasks | Pendiente |
| 3 — POST /tasks | Pendiente |
| 4 — PUT /tasks | Pendiente |
| 5 — DELETE /tasks | Pendiente |
| 6 — Swagger | Pendiente |
| 7 — Error handling | Pendiente |
| 8 — CI/CD | Pendiente |
| 9 — Cierre | Pendiente |

## Bloqueos

No hay bloqueos técnicos en este momento.

## Última sesión (2026-06-06, sesión 2)

- se creó la solución y el proyecto API
- se escribió el primer test de integración con WebApplicationFactory
- se verificó que `dotnet build` y `dotnet test` pasan
- se instaló el runtime ASP.NET Core faltante en Arch Linux

## Siguiente paso

Arrancar Fase 2 (GET /tasks).
