# Dependency Injection (DI)

## Qué es

La **inyección de dependencias** es un patrón donde una clase **recibe** sus dependencias desde afuera en vez de crearlas internamente.

```csharp
// ❌ MAL: el service crea su propia dependencia
public class TaskService
{
    private readonly InMemoryTaskRepository _repository = new();
}

// ✅ BIEN: la dependencia se inyecta por constructor
public class TaskService(ITaskRepository repository)
{
    _repository = repository;
}
```

## Cómo se registra en Program.cs

```csharp
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
```

Cada línea le dice al contenedor de DI: _"cuando alguien pida `ITaskRepository`, dale un `InMemoryTaskRepository`"_.

## Lifetimes (ciclo de vida)

| Lifetime | Crea una instancia... | ¿Cuándo usarlo? |
|---|---|---|
| `AddSingleton` | Una sola vez para toda la aplicación | Estado compartido (repositorio en memoria, caché, configuración) |
| `AddScoped` | Una por cada request HTTP | Servicios de aplicación, DbContext de Entity Framework |
| `AddTransient` | Una cada vez que se pide | Servicios livianos y sin estado, helpers |

### ¿Por qué Singleton para el repositorio?

El `InMemoryTaskRepository` guarda las tareas en una **lista en memoria**. Si fuera `Scoped`, cada request HTTP tendría **su propia lista vacía** y las tareas creadas en un request no existirían en el siguiente.

### ¿Por qué Scoped para el servicio?

`TaskService` no tiene estado, pero por convención los servicios de aplicación se registran como `Scoped`. Si en el futuro el servicio necesita un `DbContext` (que es `Scoped`), ambos coinciden en el mismo ciclo de vida.

## Cómo fluye la inyección en Fase 2

```
Program.cs registra:
  ITaskRepository → InMemoryTaskRepository (Singleton)
  ITaskService    → TaskService (Scoped)

TasksController pide ITaskService en su constructor
  → DI le da un TaskService
    → TaskService pide ITaskRepository en su constructor
      → DI le da el InMemoryTaskRepository (el mismo para toda la app)
```

## Por qué desacoplar con interfaces

```csharp
// Controller solo conoce la abstracción
public class TasksController(ITaskService taskService)

// Service solo conoce la abstracción del repositorio
public class TaskService(ITaskRepository repository)
```

Ventajas:
- **Testabilidad**: podés pasar mocks en vez de la implementación real
- **Flexibilidad**: podés cambiar `InMemoryTaskRepository` por `SqlServerTaskRepository` sin tocar ni el Service ni el Controller
- **Separación de responsabilidades**: cada capa solo depende de contratos, no de implementaciones concretas

## Error común

- Poner `AddSingleton` para todo → problemas con estado compartido no deseado.
- Poner `AddTransient` para servicios con estado interno → cada inyección recibe una instancia distinta.
- Olvidar registrar una dependencia → error en tiempo de ejecución (`InvalidOperationException: No service for type X`).
- Crear dependencias con `new()` dentro de un service → acoplamiento rígido, imposible de testear.

## Relación con el resto del sistema

- `Program.cs` es el **Composition Root**: el único lugar donde se decide qué implementación concreta usa cada abstracción
- Los **Controllers** nunca crean servicios, los reciben por inyección
- Los **Services** nunca crean repositorios, los reciben por inyección
