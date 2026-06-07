# Organización por Feature (Feature-First)

## Qué es

En lugar de organizar el código por **capas técnicas** (todos los controllers juntos, todos los modelos juntos, etc.), lo organizamos por **features del negocio** (cada funcionalidad tiene su propia carpeta con todo lo que necesita).

## Cómo se ve en el proyecto

```
Controllers/
└── TasksController.cs          ← punto de entrada HTTP

Features/
└── Tasks/                      ← feature completa
    ├── Contracts/              ← interfaces del feature
    │   ├── ITaskRepository.cs
    │   └── ITaskService.cs
    ├── Domain/                 ← modelos de dominio
    │   └── TaskItem.cs
    ├── Dtos/                   ← DTOs de entrada y salida
    │   └── TaskResponse.cs
    └── Services/               ← lógica de aplicación
        ├── ITaskService.cs
        └── TaskService.cs

Infrastructure/
└── InMemory/                   ← implementaciones concretas
    └── InMemoryTaskRepository.cs
```

## Feature-first vs Capas tradicionales

### ❌ Organización por capas (tradicional)

```
Controllers/
├── TasksController.cs
├── UsersController.cs
└── ProductsController.cs

Models/
├── Task.cs
├── User.cs
└── Product.cs

Services/
├── TaskService.cs
├── UserService.cs
└── ProductService.cs
```

**Problema**: para entender el feature "Tasks" tenés que abrir 3 carpetas distintas. Cuando el proyecto crece, cada carpeta tiene decenas de archivos y es difícil navegar.

### ✅ Feature-first

```
Features/
├── Tasks/
│   ├── Domain/TaskItem.cs
│   ├── Contracts/ITaskRepository.cs
│   ├── Dtos/TaskResponse.cs
│   └── Services/TaskService.cs
├── Users/
│   ├── Domain/User.cs
│   ├── Contracts/IUserRepository.cs
│   └── Services/UserService.cs
└── Products/
    └── ...
```

**Ventaja**: todo lo relacionado a "Tasks" está en una sola rama del árbol. Podés trabajar en un feature completo sin saltar entre carpetas.

## ¿Y los controllers?

Los controllers están en `Controllers/` (no dentro del feature) porque:

1. Son el **punto de entrada HTTP**, no parte de la lógica del feature
2. Si el feature necesita otro tipo de entrada (gRPC, GraphQL), el controller HTTP no debería estar mezclado
3. ASP.NET Core espera controllers en una carpeta convencional

## Feature-first no es Clean Architecture

| Enfoque | Separación | Complejidad |
|---|---|---|
| Feature-first | Por feature del negocio | Baja |
| Clean Architecture | Por capas concéntricas (domain, application, infrastructure) | Alta |
| Capas planas | Por capa técnica (controllers, models, services) | Media |

Elegimos **feature-first** porque es el equilibrio justo para aprender: da orden sin la abstracción pesada de Clean Architecture.

## Error común

- Poner toda la lógica en un solo archivo gigante por feature → aunque esté en una carpeta, si es un solo archivo de 500 líneas, no hay organización real.
- Mezclar features: que `TaskService` importe cosas de `Users/Domain/` → crea acoplamiento entre features.
- Poner código de infraestructura (base de datos, HTTP clients) dentro del feature → mezcla responsabilidades. La infraestructura va en `Infrastructure/`.

## Relación con el resto del sistema

```
Cada feature es una "caja" que contiene:
- Su modelo de dominio
- Sus contratos (interfaces)
- Sus DTOs
- Su servicio

La infraestructura es global:
- Un repositorio concreto implementa el contrato del feature
- El feature no sabe si el repositorio es en memoria, SQL Server o MongoDB
```
