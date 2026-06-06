# TodoApi.Api

Carpeta reservada para la futura aplicación **ASP.NET Core Web API** del proyecto.

## Estado

Esta carpeta todavía no contiene la implementación de la API.  
Solo define la estructura que se usará cuando empiece el desarrollo.

## Qué debe vivir aquí

| Carpeta | Propósito |
|---|---|
| `Controllers/` | Endpoints HTTP y manejo de requests/responses |
| `Features/Tasks/` | Todo lo relacionado con la funcionalidad de tareas |
| `Features/Tasks/Dtos/` | Contratos de entrada y salida |
| `Features/Tasks/Domain/` | Modelo de dominio simple |
| `Features/Tasks/Services/` | Reglas de negocio y casos de uso |
| `Features/Tasks/Contracts/` | Interfaces y contratos del feature |
| `Infrastructure/InMemory/` | Persistencia temporal en memoria |
| `Extensions/` | Extensiones de configuración, DI y Swagger |

## Regla

Los controladores no deben contener lógica de negocio pesada.  
La lógica del caso de uso debe vivir fuera del controller.

