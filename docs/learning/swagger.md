# Swagger / OpenAPI

## Qué es

**Swagger** es un conjunto de herramientas que implementan el estándar **OpenAPI**.  
OpenAPI es un formato JSON/YAML que describe toda una API REST: endpoints, métodos, parámetros, modelos de datos, códigos de respuesta.

Swagger consta de dos partes:

1. **Documento OpenAPI** (`/swagger/v1/swagger.json`): archivo JSON que describe la API de forma estructurada
2. **Swagger UI** (`/swagger`): interfaz web interactiva que consume ese JSON y permite explorar y probar los endpoints

## Cómo funciona

Swagger no se escribe a mano. El paquete **Swashbuckle.AspNetCore** inspecciona el código en tiempo de ejecución:

- Lee los controllers (`[ApiController]`, `[Route]`, `[HttpGet]`, etc.)
- Lee los DTOs (`TaskResponse`, `CreateTaskRequest`, etc.) y sus propiedades
- Lee los códigos de respuesta (`200`, `201`, `204`, `400`, `404`)

Y genera el documento OpenAPI automáticamente. Cada vez que se solicita `/swagger/v1/swagger.json` se regenera desde cero.

## Para qué sirve aquí

- **Probar endpoints** sin escribir cliente extra (curl, Postman)
- **Ver contratos**: qué datos espera cada endpoint y qué devuelve
- **Depurar**: Swagger UI muestra request, response, headers, y el comando curl equivalente
- **Documentación viva**: siempre sincronizada con el código

## Cómo se usa

1. Ejecutar la API: `dotnet run --project src/TodoApi.Api`
2. Abrir navegador en `http://localhost:5158/swagger`
3. Hacer clic en un endpoint → **Try it out** → **Execute**

## Personalización

Se configura en `Program.cs` dentro de `AddSwaggerGen()`:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Description = "API REST para gestionar tareas",
        Version = "v1.0.0"
    });
});
```

Los metadatos (title, description, version) se muestran en la cabecera de Swagger UI y quedan incluidos en el documento OpenAPI.

## Schemas (modelos de datos)

Swagger documenta automáticamente los DTOs en la sección `components/schemas` del documento OpenAPI.  
Cada propiedad muestra su tipo, formato y restricciones (`minLength`, `required`, etc.).

## Content-Type: application/json vs text/json vs text/plain

Swagger muestra múltiples formatos porque ASP.NET Core los expone automáticamente.  
El contenido es el mismo JSON en todos los casos. La diferencia es solo el header `Content-Type`:

| Content-Type | Uso |
|---|---|
| `application/json` | Estándar oficial (RFC 8259) |
| `text/json` | No oficial, algunos clientes lo tratan como texto |
| `text/plain` | Se devuelve como texto genérico |

Usar siempre `application/json`.

## Error común

Tratar Swagger como si fuera solo una UI bonita.  
En realidad, el valor real está en el documento OpenAPI: es un contrato machine-readable que permite generar clientes, hacer validación automatizada y documentar sin esfuerzo manual.

## Relación con el resto del sistema

Swagger refleja controllers, DTOs y rutas, por lo que debe mantenerse alineado con ellos.  
Los tests de integración en `SwaggerEndpointTests.cs` verifican que el documento OpenAPI esté completo y correcto.

## Instalación

```bash
dotnet add src/TodoApi.Api/TodoApi.Api.csproj package Swashbuckle.AspNetCore
```

Requiere `using Microsoft.OpenApi;` en Program.cs (namespace de OpenApiInfo en v2.x, no `Microsoft.OpenApi.Models`).

