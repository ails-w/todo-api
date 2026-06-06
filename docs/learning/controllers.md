# Controllers

## Qué son

Los controllers reciben requests HTTP y devuelven responses HTTP.

## Para qué sirven aquí

Serán la puerta de entrada de los endpoints de tareas:

- `GET /tasks`
- `GET /tasks/{id}`
- `POST /tasks`
- `PUT /tasks/{id}`
- `DELETE /tasks/{id}`

## Error común

Poner lógica de negocio dentro del controller.  
Eso vuelve el código difícil de probar y de mantener.

## Relación con el resto del sistema

El controller debe delegar en servicios y trabajar con DTOs.

