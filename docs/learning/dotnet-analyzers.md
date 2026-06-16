# .editorconfig y analizadores integrados de .NET

> Concepto aprendido en Fase 5 — CI/CD real + Linting.

## EditorConfig

EditorConfig **no es una librería ni un paquete NuGet**. Es un **formato de archivo estándar** que los editores/IDEs entienden de forma nativa (VS Code, Visual Studio, Rider, vim, etc.).

El archivo `.editorconfig` define convenciones de estilo para el proyecto:

- `indent_style` — espacios vs tabs
- `indent_size` — cantidad de espacios por nivel
- `end_of_line` — LF (Unix) vs CRLF (Windows)
- `charset` — codificación de caracteres
- `trim_trailing_whitespace` — eliminar espacios al final de línea
- `insert_final_newline` — asegurar que el archivo termine con un salto de línea

## Reglas específicas de .NET (`dotnet_*` y `csharp_*`)

Vienen de los **analizadores de Roslyn** — el compilador de C# en sí. No hay que instalar nada extra, el SDK de .NET ya los incluye.

| Prefijo | Qué analiza | Ejemplo |
|---------|-------------|---------|
| `dotnet_*` | Convenciones generales de .NET | `dotnet_style_readonly_field` |
| `csharp_style_*` | Estilo de C# | `csharp_style_var_for_built_in_types` |
| `csharp_new_line_*` | Posición de llaves y saltos de línea | `csharp_new_line_before_open_brace` |
| `csharp_space_*` | Espaciado entre tokens | `csharp_space_after_cast` |
| `csharp_indent_*` | Indentación de construcciones | `csharp_indent_case_contents` |

## Severidad de las reglas

Las reglas pueden tener 4 niveles de severidad:

| Severidad | Qué hace |
|-----------|----------|
| `silent` | Solo aplica al formatear documento manualmente |
| `suggestion` | Muestra un puntito gris en el IDE |
| `warning` | Muestra una advertencia en el build |
| `error` | Rompe el build |

Se configuran así en el `.editorconfig`: `regla = valor:severidad`

Ejemplo: `csharp_style_var_for_built_in_types = true:suggestion`

## Directory.Build.props

`Directory.Build.props` es un archivo que **MSBuild importa automáticamente** en todos los `.csproj` de la carpeta donde está y sus subcarpetas. Ponés uno en la raíz y aplica a toda la solución.

### Propiedades clave para análisis

```xml
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
```

Convierte las reglas del `.editorconfig` en advertencias de **build**. Sin esto, `dotnet build` ignora el `.editorconfig`.

```xml
<EnableNETAnalyzers>true</EnableNETAnalyzers>
```

Activa los analizadores de .NET (reglas `CA*`: seguridad, performance, buenas prácticas).

```xml
<AnalysisLevel>latest</AnalysisLevel>
```

Usa la versión más nueva de todas las reglas de análisis disponibles en el SDK.

## Diferencia entre IDE* y CA*

| Prefijo | Qué analizan | Ejemplo |
|---------|-------------|---------|
| `IDE*` | **Estilo** — cómo escribís el código | `IDE0001` — simplificar nombre |
| `CA*` | **Corrección/buenas prácticas** — qué escribís | `CA2007` — ConfigureAwait |

## Cómo funciona en conjunto

Archivo | Rol
---------|-----
`.editorconfig` | **Define las reglas**: indentación, naming, estilos, formateo
`Directory.Build.props` | **Activa los analizadores**: hace que las reglas se ejecuten durante el build

Uno sin el otro es medio inútil:
- Solo `.editorconfig` → el IDE muestra puntitos, pero `dotnet build` pasa igual
- Solo `Directory.Build.props` → activa analizadores, pero sin reglas personalizadas
- Juntos → **el build exige el estilo**

## dotnet format

`dotnet format` es un comando del SDK que aplica o verifica las reglas del `.editorconfig`:

```bash
dotnet format                    # Corrige automáticamente
dotnet format --verify-no-changes  # Solo verifica (ideal para CI)
```

## Recursos

- [EditorConfig.org](https://editorconfig.org)
- [Code-style rule options (Microsoft)](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/code-style-rule-options)
- [.NET analyzers overview](https://learn.microsoft.com/dotnet/fundamentals/code-analysis/overview)

→ `docs/learning/dotnet-analyzers.md`
