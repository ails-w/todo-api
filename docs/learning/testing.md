# Testing con xUnit y WebApplicationFactory

## Qué es xUnit

xUnit es un framework de pruebas para .NET. Es el más usado en el ecosistema .NET y el que viene por defecto con `dotnet new xunit`.

Los elementos clave son:

- `[Fact]` — marca un método como prueba (sin parámetros)
- `[Theory]` — marca una prueba parametrizada
- `Assert` — clase con métodos para verificar resultados (Equal, True, Null, etc.)

## Qué es WebApplicationFactory

`WebApplicationFactory<T>` es una clase del paquete `Microsoft.AspNetCore.Mvc.Testing` que **levanta tu aplicación ASP.NET Core en memoria** para hacer pruebas de integración sin necesidad de abrir un puerto real.

```
Test ──> WebApplicationFactory ──> HttpClient ──> API en memoria ──> Response
```

### Ventajas

- no necesita puertos ni procesos separados
- comparte el mismo proceso que los tests
- permite configurar servicios mock antes de cada test
- es más rápido que levantar la API real

## Ciclo TDD (Test-Driven Development)

1. **🔴 Rojo**: escribir el test antes del código. El test falla porque no hay código.
2. **🟢 Verde**: escribir el código mínimo necesario para que el test pase.
3. **🔵 Refactor**: mejorar el código sin cambiar su comportamiento.

```
Escribir test ─> Test falla ─> Escribir código ─> Test pasa ─> Refactor
     (rojo)                      (verde)
```

### En Fase 1 aplicamos TDD así:

Escribimos `ApiHealthCheckTests` que verifica que GET /weatherforecast devuelve 200 OK. El endpoint ya existía del template (el test "pasa a verde" inmediatamente). En fases siguientes escribiremos tests **antes** de crear los endpoints (rojo → verde).

## Cómo se usa aquí

```csharp
// Arrange: crear la fábrica y el cliente HTTP
await using var factory = new WebApplicationFactory<Program>();
using var client = factory.CreateClient();

// Act: ejecutar la petición
var response = await client.GetAsync("/weatherforecast");

// Assert: verificar el resultado
Assert.Equal(HttpStatusCode.OK, response.StatusCode);
```

### InternalsVisibleTo

Para que `WebApplicationFactory<Program>` funcione, el proyecto de tests necesita ver el tipo `Program` (que es interno en .NET). Se configura en el `.csproj`:

```xml
<ItemGroup>
  <InternalsVisibleTo Include="TodoApi.Tests" />
</ItemGroup>
```

## Error común

- Olvidar `InternalsVisibleTo` → error `CS0246: WebApplicationFactory<> not found`
- Confundir `WebApplicationFactory` con `TestServer` (TestServer es más bajo nivel)
- Usar puertos reales para tests de integración cuando `WebApplicationFactory` es más simple

## Relación con el resto del sistema

- Cada feature tendrá sus propios tests de integración
- Los tests son la red de seguridad que permite refactorizar sin miedo
- TDD nos obliga a pensar en el diseño antes de escribir código
