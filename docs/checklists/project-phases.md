# Guía de fases del proyecto — Todo API

Esta guía organiza el proyecto completo en fases secuenciales.  
Cada fase contiene tareas atómicas y verificables con checkbox para seguimiento entre sesiones.

**Regla de trabajo:** no se salta una fase sin completar la anterior.  
**Regla de aprendizaje:** cada tarea incluye entender el concepto antes de escribir código.  
**Regla de documentación:** cada fase deja rastro en `docs/progress/`, `docs/learning/` y `docs/project-state.md`.

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

## Fase 3 — Feature Tasks: POST /tasks (crear tarea)

- [ ] **3.1** Crear `CreateTaskRequest` (DTO de entrada con validación)
- [ ] **3.2** Agregar validación con anotaciones (`[Required]`, etc.)
- [ ] **3.3** Agregar `CreateAsync` a `ITaskRepository`
- [ ] **3.4** Implementar `CreateAsync` en `InMemoryTaskRepository`
- [ ] **3.5** Agregar `CreateAsync` a `TaskService` (mapeo DTO → dominio)
- [ ] **3.6** Agregar `POST /api/tasks` al controller
- [ ] **3.7** **Prueba**: POST válido → 201 Created + Location header
- [ ] **3.8** **Prueba**: POST inválido → 400 Bad Request
- [ ] **3.9** Documentar conceptos en `docs/learning/`
- [ ] **3.10** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- HTTP POST y creación de recursos
- 201 Created y Location header
- Validación automática con Data Annotations
- Mapeo DTO → dominio

---

## Fase 4 — Feature Tasks: PUT /tasks/{id} (actualizar tarea)

- [ ] **4.1** Crear `UpdateTaskRequest` con validación
- [ ] **4.2** Agregar `UpdateAsync` a `ITaskRepository`
- [ ] **4.3** Implementar `UpdateAsync` en `InMemoryTaskRepository`
- [ ] **4.4** Agregar `UpdateAsync` a `TaskService`
- [ ] **4.5** Agregar `PUT /api/tasks/{id}` al controller
- [ ] **4.6** **Prueba**: PUT válido → 200 + tarea actualizada
- [ ] **4.7** **Prueba**: PUT inexistente → 404
- [ ] **4.8** **Prueba**: PUT inválido → 400
- [ ] **4.9** Documentar conceptos en `docs/learning/`
- [ ] **4.10** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- HTTP PUT vs PATCH (idempotencia)
- Actualización completa del recurso
- 200 OK vs 204 NoContent

---

## Fase 5 — Feature Tasks: DELETE /tasks/{id} (eliminar tarea)

- [ ] **5.1** Agregar `DeleteAsync` a `ITaskRepository`
- [ ] **5.2** Implementar `DeleteAsync` en `InMemoryTaskRepository`
- [ ] **5.3** Agregar `DeleteAsync` a `TaskService`
- [ ] **5.4** Agregar `DELETE /api/tasks/{id}` al controller
- [ ] **5.5** **Prueba**: DELETE existente → 204 NoContent
- [ ] **5.6** **Prueba**: DELETE inexistente → 404
- [ ] **5.7** Documentar conceptos en `docs/learning/`
- [ ] **5.8** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- HTTP DELETE semántica
- 204 NoContent
- Decisiones de diseño: error vs silencio en delete

---

## Fase 6 — Swagger / OpenAPI

- [ ] **6.1** Explorar el Swagger incluido en el template
- [ ] **6.2** Personalizar título y descripción de la API
- [ ] **6.3** Verificar schemas de DTOs en Swagger UI
- [ ] **6.4** Probar todos los endpoints desde Swagger UI
- [ ] **6.5** Documentar conceptos en `docs/learning/`
- [ ] **6.6** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- ¿Qué es OpenAPI / Swagger?
- Documentación automática de endpoints
- Swagger UI como herramienta de exploración

---

## Fase 7 — Error handling global

- [ ] **7.1** Crear `ExceptionMiddleware`
- [ ] **7.2** Mapear `KeyNotFoundException` → 404
- [ ] **7.3** Capturar errores no controlados → 500 genérico (sin stack trace)
- [ ] **7.4** Registrar middleware en el pipeline (Program.cs)
- [ ] **7.5** **Prueba**: ruta inexistente → 404
- [ ] **7.6** **Prueba**: error interno → 500 (sin datos sensibles)
- [ ] **7.7** Documentar conceptos en `docs/learning/`
- [ ] **7.8** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- Middleware pipeline en ASP.NET Core
- Excepciones vs resultados HTTP
- Seguridad: no exponer stack traces

---

## Fase 8 — CI/CD real

- [ ] **8.1** Reemplazar `ci.yml` placeholder con build real en push y PR
- [ ] **8.2** Reemplazar `test.yml` placeholder con `dotnet test`
- [ ] **8.3** Verificar que los workflows corren en GitHub
- [ ] **8.4** Actualizar `docs/progress/` y `docs/project-state.md`

### Conceptos a aprender

- GitHub Actions básico
- Triggers: push y pull_request
- Matriz de tests

---

## Fase 9 — Cierre y documentación final

- [ ] **9.1** Revisar que todas las fases estén completas
- [ ] **9.2** Actualizar `docs/project-state.md` con resumen final
- [ ] **9.3** Documentar último progreso en `docs/progress/`
- [ ] **9.4** Revisar `docs/learning/` — documentar conceptos pendientes
- [ ] **9.5** Dejar handoff listo para próxima etapa (si aplica)

---

## Resumen de progreso

| Fase | Tareas | Estado |
|---|---|---|
| 0 — Fundaciones | 5 | ✅ |
| 1 — Scaffolding | 10 + docs | ✅ |
| 2 — GET /tasks | 14 | ✅ |
| 3 — POST /tasks | 10 | ⏳ |
| 4 — PUT /tasks | 10 | ⏳ |
| 5 — DELETE /tasks | 8 | ⏳ |
| 6 — Swagger | 6 | ⏳ |
| 7 — Error handling | 8 | ⏳ |
| 8 — CI/CD | 4 | ⏳ |
| 9 — Cierre | 5 | ⏳ |

---

*Este archivo es la fuente de verdad para navegar el proyecto por fases.*  
*Se actualiza al completar cada tarea. Los checkboxes permiten seguimiento entre sesiones.*
