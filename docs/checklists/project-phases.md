# Guía de fases del proyecto — Todo API

Esta guía organiza el proyecto completo en fases secuenciales.  
Cada fase contiene tareas atómicas y verificables con checkbox para seguimiento entre sesiones.

**Regla de trabajo:** no se salta una fase sin completar la anterior.  
**Regla de aprendizaje:** cada tarea incluye entender el concepto antes de escribir código.  
**Regla de documentación:** cada fase deja rastro en `docs/progress/`, `docs/learning/` y `docs/project-state.md`.  
**Regla de conceptos:** cada ítem en "Conceptos a aprender" debe incluir la ruta al archivo en `docs/learning/` donde se documentó, con el formato `→ docs/learning/<archivo>.md`.
**Regla de commits y PR:** cada fase incluye al menos **2 commits de avance** propuestos por el asistente. Al completar la implementación y documentación, el chat propondrá una **Pull Request** con los cambios acumulados de la fase.

---

## Fase 0 — Fundaciones ✅ *(completada)*

- [x] Estructura documental del repositorio
- [x] Mapa central del proyecto (`docs/index.md`)
- [x] Decisión de arquitectura (`docs/architecture/0001-architecture.md`)
- [x] Checklists operativos (sesión, implementación, revisión, merge, cierre)
- [x] Plantilla de PR y workflows placeholder

---

## Fase 1 — Scaffolding: solución y proyecto

### Actividades

- [x] **1.1** Crear solución .NET (`TodoApi.slnx`)
- [x] **1.2** Crear proyecto Web API (`src/TodoApi.Api/TodoApi.Api.csproj`)
- [x] **1.3** Explorar los archivos que genera el template (Program.cs, launchSettings, etc.)
- [x] **1.4** Ejecutar `dotnet build` y verificar que compile
- [x] **1.5** Crear proyecto xUnit de tests y referenciar la API
- [x] **1.6** Agregar paquete `Microsoft.AspNetCore.Mvc.Testing`
- [x] **1.7** Hacer visible `Program` para tests via `InternalsVisibleTo`
- [x] **1.8** Escribir test de health check con TDD (GET /weatherforecast → 200 OK)
- [x] **1.9** Instalar runtime ASP.NET Core faltante (entorno Arch Linux)
- [x] **1.10** Ejecutar `dotnet test` y verificar que pase ✅

### Conceptos aprendidos

- [x] ¿Qué es una solución .NET y para qué sirve? → `docs/learning/dotnet-solution.md`
- [x] Estructura de un proyecto ASP.NET Core Web API → `docs/learning/aspnet-core-project.md`
- [x] ¿Qué hace cada archivo del template? → `docs/learning/aspnet-core-project.md`
- [x] ¿Por qué separar tests del código de producción? → `docs/learning/testing.md`
- [x] ¿Qué es WebApplicationFactory? → `docs/learning/testing.md`
- [x] Ciclo TDD: rojo → verde → refactor → `docs/learning/testing.md`

### Documentación

- [x] Actualizar `docs/progress/` con avance de la sesión
- [x] Actualizar `docs/project-state.md` con nuevo estado
- [x] Documentar conceptos aprendidos en `docs/learning/`

---

## Fase 2 — Feature Tasks: modelo de dominio y GET /tasks ✅ *(completada)*

- [x] **2.1** Crear `TaskItem` (modelo de dominio con Id, Title, IsCompleted)
- [x] **2.2** Crear interfaz `ITaskRepository` (contrato para el repositorio)
- [x] **2.3** Implementar `InMemoryTaskRepository` con datos semilla
- [x] **2.4** Crear `TaskResponse` (DTO de salida)
- [x] **2.5** Crear `TaskService` con `GetAllAsync`
- [x] **2.6** Agregar `GetByIdAsync` al servicio
- [x] **2.7** Crear `TasksController` con `GET /api/tasks`
- [x] **2.8** Agregar `GET /api/tasks/{id}` al controller
- [x] **2.9** Registrar dependencias en DI (Program.cs)
- [x] **2.10** **Prueba**: GET /tasks → 200 + lista JSON
- [x] **2.11** **Prueba**: GET /tasks/{id} existente → 200 + tarea
- [x] **2.12** **Prueba**: GET /tasks/{id} inexistente → 404
- [x] **2.13** Documentar conceptos en `docs/learning/`
- [x] **2.14** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos aprendidos

- [x] Modelo de dominio vs DTO → `docs/learning/dto.md`
- [x] ¿Qué es un contrato/interfaz y por qué desacoplar? → `docs/learning/interfaces.md`
- [x] Inyección de dependencias (motivación y registro) → `docs/learning/dependency-injection.md`
- [x] DTO de salida vs entrada → `docs/learning/dto.md`
- [x] Organización por feature (feature-first) → `docs/learning/feature-first.md`
- [x] Códigos HTTP: 200, 404 → `docs/learning/http-methods.md`
- [x] Routing por atributos y route constraints → `docs/learning/routing.md`
- [x] Controllers y ActionResult<T> → `docs/learning/controllers.md`

---

## Fase 3 — POST /tasks

- [x] **[TEST]** POST válido → 201 + Location (escribir test → falla)
- [x] **[TEST]** POST inválido → 400 (escribir test → falla)
- [x] Crear `CreateTaskRequest` con validación
- [x] Agregar `CreateAsync` a `ITaskRepository`
- [x] Implementar `CreateAsync` en `InMemoryTaskRepository`
- [x] Agregar `CreateAsync` a `ITaskService` + `TaskService`
- [x] Agregar `POST /api/tasks` al controller
- [x] → Tests GREEN
- [x] Documentar conceptos en `docs/learning/`
- [x] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos aprendidos

- [x] HTTP POST y creación de recursos → `docs/learning/http-methods.md`
- [x] 201 Created y Location header → `docs/learning/http-methods.md`
- [x] Validación automática con Data Annotations → `docs/learning/validation.md`
- [x] Mapeo DTO → dominio → `docs/learning/dto.md`

---

## Fase 4 — PUT /tasks/{id} ✅ *(completada)*

- [x] **[TEST]** PUT válido → 200 + tarea actualizada (escribir test → falla)
- [x] **[TEST]** PUT inexistente → 404 (escribir test → falla)
- [x] **[TEST]** PUT inválido → 400 (escribir test → falla)
- [x] Crear `UpdateTaskRequest` con validación
- [x] Agregar `UpdateAsync` a `ITaskRepository`
- [x] Implementar `UpdateAsync` en `InMemoryTaskRepository`
- [x] Agregar `UpdateAsync` a `ITaskService`
- [x] Agregar `PUT /api/tasks/{id}` al controller
- [x] → Tests GREEN
- [x] Documentar conceptos en `docs/learning/`
- [x] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos aprendidos

- [x] HTTP PUT vs PATCH (idempotencia y actualización completa) → `docs/learning/http-methods.md`
- [x] 200 OK vs 204 NoContent → `docs/learning/http-methods.md`

---

## Fase 5 — CI/CD real + Linting

- [x] Crear `.editorconfig` en la raíz con reglas básicas de estilo .NET
- [x] Agregar `Directory.Build.props` con `EnforceCodeStyleInBuild`
- [x] Reemplazar `.github/workflows/ci.yml` placeholder con build real (push + PR)
- [x] Reemplazar `.github/workflows/test.yml` placeholder con `dotnet test`
- [x] Agregar `dotnet format --verify-no-changes` al workflow de CI
- [x] Commit 1: configuración de linting (.editorconfig + Directory.Build.props)
- [x] Commit 2: workflows de CI/CD reales (ci.yml + test.yml)
- [x] **[TEST]** Verificar que los workflows corren en GitHub (push y PR)
- [x] → Pedir al chat una propuesta de PR con los cambios de la fase
- [x] Documentar conceptos en `docs/learning/` (GitHub Actions)
- [x] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos aprendidos

- [x] GitHub Actions básico (triggers, jobs, steps) → `docs/learning/github-actions.md`
- [x] Triggers: push y pull_request → `docs/learning/github-actions.md`
- [x] `dotnet format` como verificación de estilo en CI → `docs/learning/github-actions.md`
- [x] `.editorconfig` y analizadores integrados de .NET → `docs/learning/dotnet-analyzers.md`

---

## Fase 6 — DELETE /tasks/{id} ✅ *(completada)*

- [x] **[TEST]** DELETE existente → 204 NoContent (escribir test → falla)
- [x] **[TEST]** DELETE inexistente → 404 (escribir test → falla)
- [x] Agregar `DeleteAsync` a `ITaskRepository`
- [x] Implementar `DeleteAsync` en `InMemoryTaskRepository`
- [x] Agregar `DeleteAsync` a `ITaskService`
- [x] Agregar `DELETE /api/tasks/{id}` al controller
- [x] → Tests GREEN
- [x] Commit 1: implementación de DELETE (repositorio + servicio + controller)
- [x] Commit 2: tests + documentación de DELETE
- [x] → Pedir al chat una propuesta de PR con los cambios acumulados desde el último PR
- [x] Documentar conceptos en `docs/learning/`
- [x] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos aprendidos

- [x] HTTP DELETE semántica → `docs/learning/http-methods.md`
- [x] 204 NoContent → `docs/learning/http-methods.md`
- [x] Decisiones de diseño: error vs silencio en delete → `docs/learning/http-methods.md`

---

## Fase 7 — Swagger / OpenAPI ✅ *(completada)*

- [x] **[TEST]** Explorar Swagger UI y verificar endpoints listados
- [x] Personalizar título y descripción de la API
- [x] Verificar schemas de DTOs en Swagger UI
- [x] Probar todos los endpoints desde Swagger UI
- [ ] Commit 1: configuración de Swagger (paquete + Program.cs)
- [ ] Commit 2: personalización + verificación en Swagger UI
- [ ] → Pedir al chat una propuesta de PR con los cambios acumulados desde el último PR
- [x] Documentar conceptos en `docs/learning/` → `docs/learning/swagger.md`
- [ ] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- [x] ¿Qué es OpenAPI / Swagger? → `docs/learning/swagger.md`
- [x] Documentación automática de endpoints
- [x] Swagger UI como herramienta de exploración

---

## Fase 8 — Error handling global

- [ ] **[TEST]** Ruta inexistente → 404 (escribir test → falla)
- [ ] **[TEST]** Error interno → 500 sin datos sensibles (escribir test → falla)
- [ ] Crear `ExceptionMiddleware`
- [ ] Mapear `KeyNotFoundException` → 404
- [ ] Capturar errores no controlados → 500 genérico (sin stack trace)
- [ ] Registrar middleware en el pipeline (Program.cs)
- [ ] → Tests GREEN
- [ ] Commit 1: implementación de ExceptionMiddleware
- [ ] Commit 2: tests + documentación de error handling
- [ ] → Pedir al chat una propuesta de PR con los cambios acumulados desde el último PR
- [ ] Documentar conceptos en `docs/learning/`
- [ ] Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- [ ] Middleware pipeline en ASP.NET Core
- [ ] Excepciones vs resultados HTTP
- [ ] Seguridad: no exponer stack traces

---

## Fase 9 — Cierre y documentación final

- [ ] Revisar que todas las fases estén completas
- [ ] Actualizar `docs/project-state.md` con resumen final
- [ ] Documentar último progreso en `docs/progress/`
- [ ] Revisar `docs/learning/` — documentar conceptos pendientes
- [ ] Commit 1: actualización de documentación final
- [ ] Commit 2: handoff listo para próxima etapa
- [ ] → Pedir al chat una propuesta de PR final con todo el proyecto
- [ ] Dejar handoff listo para próxima etapa (si aplica)

---

## Resumen de progreso

| Fase | Tareas | Estado |
|---|---|---|
| 0 — Fundaciones | 5 | ✅ |
| 1 — Scaffolding | 10 + docs | ✅ |
| 2 — GET /tasks | 14 | ✅ |
| 3 — POST /tasks | 10 | ✅ |
| 4 — PUT /tasks | 11 | ✅ |
| 5 — CI/CD + Linting | 11 | ✅ |
| 6 — DELETE /tasks | 12 | ✅ |
| 7 — Swagger | 9 | ⏳ |
| 8 — Error handling | 12 | ⏳ |
| 9 — Cierre | 8 | ⏳ |

---

*Este archivo es la fuente de verdad para navegar el proyecto por fases.*  
*Se actualiza al completar cada tarea. Los checkboxes permiten seguimiento entre sesiones.*
