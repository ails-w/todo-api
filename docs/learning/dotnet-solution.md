# Solución .NET y proyecto

## Qué es una solución

Una solución (`.sln` o `.slnx`) es un **contenedor** que agrupa uno o más proyectos relacionados. No contiene código, solo referencias a los proyectos y su organización en carpetas lógicas.

## Para qué sirve aquí

Nuestra solución agrupa dos proyectos:

- `src/TodoApi.Api/` — la aplicación Web API
- `tests/TodoApi.Tests/` — las pruebas automatizadas

```
TodoApi.slnx
├── src/TodoApi.Api/
└── tests/TodoApi.Tests/
```

## .slnx vs .sln

| Formato | Descripción |
|---|---|
| `.sln` (legacy) | Formato binario/textual usado hasta .NET 9 |
| `.slnx` (nuevo) | Formato XML limpio introducido en .NET 10 |

.NET 10 genera `.slnx` por defecto con `dotnet new sln`. Ambos formatos son equivalentes en funcionalidad.

## Error común

Confundir solución con proyecto. La solución es el **contenedor**; el proyecto es el **código** que produce un artefacto (DLL, EXE).

## Relación con el resto del sistema

- `dotnet build` compila toda la solución
- `dotnet test` ejecuta los tests de todos los proyectos de prueba
- `dotnet sln add` agrega un proyecto existente a la solución
- La solución permite comandos que operan sobre múltiples proyectos a la vez
