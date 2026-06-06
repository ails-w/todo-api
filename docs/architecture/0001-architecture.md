# ADR 0001: API simple con organización por feature y almacenamiento en memoria

## Estado

Aceptada para esta etapa de aprendizaje.

## Contexto

Este proyecto tiene como objetivo aprender los fundamentos de ASP.NET Core, no construir desde el inicio una arquitectura excesivamente compleja.

Necesitamos una estructura que permita entender claramente:

- request HTTP
- controller
- DTO
- servicio
- almacenamiento en memoria
- response HTTP

## Decisión

Usar una **API simple** con:

- un proyecto principal ASP.NET Core Web API
- organización interna por feature
- DTOs separados del dominio
- servicios para la lógica de negocio
- almacenamiento en memoria para el aprendizaje inicial
- documentación viva en `docs/`

## Alternativas consideradas

### 1. Clean Architecture completa
**Ventaja:** buena separación y escalabilidad.  
**Desventaja:** agrega complejidad innecesaria para el objetivo actual.

### 2. Monolito plano con lógica en controllers
**Ventaja:** rapidez inicial.  
**Desventaja:** enseña malos hábitos y mezcla responsabilidades.

### 3. API simple con feature-first ligero
**Ventaja:** equilibrio entre claridad y orden.  
**Desventaja:** exige disciplina para no mezclar capas.

## Tradeoffs

Elegimos claridad y aprendizaje por encima de abstracción prematura.  
La estructura es suficientemente ordenada para crecer después, sin ocultar los fundamentos.

## Qué resuelve

- ayuda a aprender ASP.NET Core paso a paso
- evita controllers anémicos o inflados
- separa contratos, dominio y lógica
- deja documentación útil para trabajo con otro agente

## Qué no resuelve

- persistencia real en base de datos
- escalado de producción
- despliegue automático completo

## Consecuencia

Esta decisión será la base para la implementación inicial.  
Si el proyecto crece, se podrá evolucionar hacia una arquitectura más estricta sin romper el aprendizaje inicial.

