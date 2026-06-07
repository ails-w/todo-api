# Interfaces y Contratos

## Qué son

Una **interfaz** es un contrato que define **qué** hace una clase, sin decir **cómo** lo hace. En C# se declara con `interface` y contiene solo firmas de métodos, sin implementación.

```csharp
public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
}
```

Esto dice: _"quien sea un `ITaskRepository` debe saber devolver todas las tareas y devolver una por ID"_.

## Por qué desacoplar con interfaces

### 1. Podés cambiar la implementación sin tocar el consumidor

```csharp
// HOY: en memoria
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

// MAÑANA: base de datos real — solo cambia esta línea
builder.Services.AddSingleton<ITaskRepository, SqlServerTaskRepository>();
```

El `TaskService` nunca sabe qué implementación está usando. Solo conoce `ITaskRepository`. Cambiar de `InMemory` a `SqlServer` no requiere modificar ni el Service ni el Controller.

### 2. Testabilidad

```csharp
// Podés pasar un mock en los tests
var mockRepo = new Mock<ITaskRepository>();
mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(tareasFalsas);

var service = new TaskService(mockRepo.Object);
```

Sin la interfaz, los tests tendrían que usar el `InMemoryTaskRepository` real, con datos que pueden mutar entre tests.

### 3. Separación de responsabilidades

```
Controller → depende de → ITaskService (interfaz)
Service    → depende de → ITaskRepository (interfaz)
Repository → implementa → ITaskRepository
```

Cada capa solo conoce **lo mínimo necesario** para hacer su trabajo.

## Convención de nombres

En C#, las interfaces públicas por convención empiezan con `I`:

| Interfaz | Implementación |
|---|---|
| `ITaskRepository` | `InMemoryTaskRepository` |
| `ITaskService` | `TaskService` |

## Dónde vive cada cosa

```
Features/Tasks/
├── Contracts/          ← interfaces (contratos)
│   ├── ITaskRepository.cs
│   └── ITaskService.cs
├── Domain/             ← modelos de dominio
│   └── TaskItem.cs
├── Services/           ← implementaciones de servicios
│   ├── ITaskService.cs (interfaz)
│   └── TaskService.cs  (implementación)
└── Dtos/               ← DTOs de entrada/salida
    └── TaskResponse.cs

Infrastructure/         ← implementaciones de infraestructura
└── InMemory/
    └── InMemoryTaskRepository.cs
```

Notá que `ITaskRepository` (contrato) está en `Features/Tasks/Contracts/`, pero `InMemoryTaskRepository` (implementación) está en `Infrastructure/`. El contrato vive cerca del dominio, la implementación vive en infraestructura. Así el dominio **no depende** de la infraestructura.

## Error común

- Crear interfaces "porque sí" sin una razón real → sobreingeniería.
- Poner una sola implementación y forzar la interfaz "por si acaso" → YAGNI (You Aren't Gonna Need It).
- Nombrar interfaces sin la `I` → rompe la convención del ecosistema .NET.
- Poner las interfaces y las implementaciones en el mismo archivo → mezcla responsabilidades y dificulta encontrar el contrato.

## Relación con el resto del sistema

```
Program.cs registra:
  ITaskRepository → InMemoryTaskRepository  ← contrato ↔ implementación

TaskService usa:
  ITaskRepository (contrato)                ← depende de abstracción, no de contrato

InMemoryTaskRepository implementa:
  ITaskRepository (contrato)                ← la implementación concreta
```
