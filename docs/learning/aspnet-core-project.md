# Proyecto ASP.NET Core Web API

## Qué es

Un proyecto ASP.NET Core es una aplicación que corre sobre el runtime de ASP.NET y puede servir contenido HTTP (APIs, sitios web, etc.). El template `webapi` genera una API REST mínima.

## Archivos del template

| Archivo | Propósito |
|---|---|
| `Program.cs` | Punto de entrada de la aplicación. Configura servicios, middleware y endpoints |
| `TodoApi.Api.csproj` | Archivo del proyecto: target framework, dependencias, configuración de build |
| `Properties/launchSettings.json` | Configuración de depuración local (puertos, perfil, variables de entorno) |
| `appsettings.json` | Configuración general de la aplicación (cadenas de conexión, logging, etc.) |
| `appsettings.Development.json` | Configuración específica para desarrollo — pisa valores de `appsettings.json` |
| `TodoApi.Api.http` | Archivo para probar endpoints con REST Client (VS Code, Rider) |

## Program.cs y Minimal API

Desde .NET 6, el template usa **Minimal API** (sin `Startup.cs`). Todo se configura en `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);  // Configura servicios
var app = builder.Build();                           // Construye la app
app.MapGet("/ruta", () => { ... });                  // Define endpoints
app.Run();                                           // Inicia el servidor
```

En fases posteriores reemplazaremos `MapGet` por Controllers tradicionales.

## appsettings.json

Archivo de configuración jerárquico en JSON. Ejemplo:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

Los valores se acceden mediante `builder.Configuration["Seccion:Clave"]`.

## launchSettings.json

Solo se usa en desarrollo local (no en producción). Define:

- qué puerto HTTP y HTTPS usa la app
- variables de entorno (como `ASPNETCORE_ENVIRONMENT`)
- perfil de ejecución (IIS Express, proyecto, Docker)

## Error común

- Editar `appsettings.Development.json` pensando que afecta a producción (no, solo desarrollo)
- Olvidar que `launchSettings.json` no se usa en producción ni en tests
- Confundir el pipeline de middleware con el archivo de configuración

## Relación con el resto del sistema

- `Program.cs` es donde registramos los servicios (DI) y configuramos el pipeline HTTP
- `appsettings.json` puede tener configuraciones que los servicios consuman
- `launchSettings.json` define cómo se ejecuta localmente
