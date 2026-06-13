# GitHub Actions

> Concepto aprendido en Fase 5 — CI/CD real + Linting.

## ¿Qué es?

GitHub Actions es el **sistema de CI/CD integrado de GitHub**. Permite ejecutar comandos automáticamente cuando ocurren eventos en el repositorio (push, PR, etc.).

## ¿Qué es un workflow?

Un **workflow** es un archivo YAML dentro de `.github/workflows/` que define:

- **Cuándo** se ejecuta (`on:`)
- **En qué máquina** (`runs-on:`)
- **Qué comandos** ejecuta (`steps:`)

## Anatomía de un workflow

```yaml
name: CI                              # Nombre visible en GitHub

on:                                   # Triggers
  push:
    branches: [main, dev]
  pull_request:
    branches: [main]

jobs:                                 # Trabajos (corren en paralelo)
  build:                              # Nombre del job
    runs-on: ubuntu-latest            # Máquina virtual

    steps:
      - name: Checkout
        uses: actions/checkout@v4     # Acción reutilizable

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:                         # Parámetros de la acción
          dotnet-version: "10.0.x"

      - name: Build
        run: dotnet build --no-restore # Comando bash directo
```

## Triggers principales

| Trigger | Cuándo se ejecuta | Ejemplo |
|---------|-------------------|---------|
| `push` | Al hacer `git push` | `push: branches: [main]` |
| `pull_request` | Al abrir/actualizar un PR | `pull_request: branches: [main]` |
| `workflow_dispatch` | Manualmente desde GitHub | Útil para debugging |

## Jobs y steps

- **Job**: conjunto de pasos que corren en una misma máquina virtual
- **Step**: una instrucción dentro de un job (usa `uses:` para acciones o `run:` para comandos bash)
- Los steps se ejecutan **en orden**. Si uno falla, se cancela el resto del job.

## Acciones (`uses`) vs comandos (`run`)

| | `uses:` | `run:` |
|---|---|---|
| Qué es | Plugin reutilizable | Comando bash directo |
| Ejemplo | `actions/checkout@v4` | `dotnet build` |
| Quién lo mantiene | GitHub o la comunidad | Vos |
| Versionado | `@v4` (major tag) | N/A |

## Máquinas virtuales (`runs-on`)

- `ubuntu-latest` — Linux (más rápido, más barato)
- `windows-latest` — Windows Server
- `macos-latest` — macOS

## Cómo se ven en GitHub

Cada workflow aparece en la pestaña **Actions** del repositorio. Se ve el nombre, el estado (✅ / ❌ / ⏳), la rama y el tiempo de ejecución.

## Recursos

- [Documentación oficial de GitHub Actions](https://docs.github.com/en/actions)
- [Acciones oficiales de GitHub](https://github.com/actions)

→ `docs/learning/github-actions.md`
