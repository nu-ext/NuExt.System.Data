# NuExt.System.Data

`NuExt.System.Data` is a **lightweight, provider-independent data access foundation for .NET applications and application frameworks**, combining practical ADO.NET extensions, explicit database context ownership, reusable DAL abstractions, and deterministic database migration orchestration.

[![NuGet](https://img.shields.io/nuget/v/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)
[![Ask DeepWiki](https://img.shields.io/badge/Ask-DeepWiki-blue)](https://deepwiki.com/nu-ext/NuExt.System.Data)
[![Build](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml/badge.svg)](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml)
[![License](https://img.shields.io/github/license/nu-ext/NuExt.System.Data?label=license)](https://github.com/nu-ext/NuExt.System.Data/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)

## Why NuExt.System.Data?

- **Provider-independent** — common DAL and migration contracts remain separate from provider-specific connections, transactions, and history storage
- **Ownership-aware** — DAL operations explicitly distinguish owned contexts from caller-provided contexts
- **Composable** — independent operations and larger transactional units of work use the same DAL implementation
- **Migration-safe** — configured migrations and applied history are validated as one ordered sequence before pending changes run
- **ADO.NET-focused** — typed reader, row, and table helpers without an ORM or tracking model
- **Consistent across runtimes** — a stable API surface across modern .NET, .NET Standard, and .NET Framework
- **Dependency-light** — no required ORM, hosting model, dependency injection container, or database provider

## Designed for

NuExt.System.Data is intended for application and framework authors who need:

- a small reusable foundation above ADO.NET rather than a full ORM;
- explicit ownership and transaction boundaries in data access code;
- the same DAL methods for standalone operations and caller-controlled units of work;
- deterministic migration orchestration with provider-specific execution;
- strict validation of migration ordering and applied history;
- compatibility across modern .NET and legacy .NET Framework targets.

## Core infrastructure

### ADO.NET extensions

- `DataReaderExtensions` adds name-based typed accessors and nullable getters for older targets.
- `DataRowExtensions` provides typed and nullable conversions by index, name, `DataColumn`, or `DataRowVersion`.
- `DataTableExtensions` provides focused table operations without object tracking.
- Compatibility implementations resolve column names to ordinals and preserve provider behavior.

### DAL and database contexts

- `IDbContext` defines the lifetime contract for provider-specific database contexts.
- `IDbContextExecutor<TDbContext>` creates and executes owned context scopes while also supporting operations over existing contexts.
- `DalBase<TDbContext>` centralizes DAL execution and exposes the same synchronous and asynchronous operation model for owned and borrowed contexts.
- `DbContextExtensions` adds focused helpers around context execution without hiding ownership boundaries.

### Explicit context ownership

The DAL infrastructure supports two execution models:

- **Owned context** — when no context is supplied, the provider-specific executor creates and owns the context, including connection lifetime, transaction completion, and cleanup.
- **Borrowed context** — when an existing context is supplied, the DAL participates in the caller's unit of work without committing, rolling back, or disposing that context.

This design allows a DAL operation to run independently or compose with other operations inside one caller-controlled transaction without duplicating data access logic.

### Deterministic database migrations

Migration orchestration is provider-independent; transaction handling and history persistence are provider-specific.

- `DbMigration<TContext>` represents an ordered schema or data change with a stable identifier.
- `DbMigrationContext` carries cancellation and provides a base for provider-specific state.
- `DbMigrator<TContext>` validates migration configuration and history before applying pending migrations.
- `IDbMigrationExecutor<TContext>` defines the provider-specific execution and transaction boundary.
- `IDbMigrationHistory<TContext>` defines migration history storage.
- `AppliedDbMigration` records the sequence, identifier, UTC application time, and optional version.

Before executing pending migrations, the migrator verifies that:

- configured migration identifiers are non-empty and unique;
- applied history does not contain more entries than the current configuration;
- applied sequence numbers are contiguous and one-based;
- every applied identifier matches the configured migration at the same position.

This treats migration history as a strict prefix of the configured migration sequence and prevents silently continuing from reordered, removed, duplicated, or otherwise inconsistent migrations.

### Cancellation and transaction boundaries

- Cancellation is checked before execution and before each pending migration.
- The migration context passes the cancellation token to provider code and migrations.
- A migration is recorded only after its `Up` operation completes successfully.
- `IDbMigrationExecutor<TContext>` controls transactions and keeps migration history consistent.

## Compatibility

- **.NET Standard 2.0+, .NET 8/9/10, and .NET Framework 4.6.2+**
- Works with provider-specific implementations built on ADO.NET
- Includes selected ADO.NET API backports for older targets
- Does not require Entity Framework or another ORM

## Installation

Via [NuGet](https://www.nuget.org/):

```shell
dotnet add package NuExt.System.Data
```

Or via Visual Studio:

1. Go to `Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution...`.
2. Search for `NuExt.System.Data`.
3. Click "Install".

## Provider implementation

For a complete provider implementation, transaction strategy, migration history store, and usage example, see:

- [NuExt.System.Data.SQLite](https://github.com/nu-ext/NuExt.System.Data.SQLite)

## Ecosystem

- [NuExt.System](https://github.com/nu-ext/NuExt.System)
- [NuExt.System.Data.SQLite](https://github.com/nu-ext/NuExt.System.Data.SQLite)
- [NuExt.Minimal.Mvvm](https://github.com/nu-ext/NuExt.Minimal.Mvvm)
- [NuExt.Minimal.Mvvm.SourceGenerator](https://github.com/nu-ext/NuExt.Minimal.Mvvm.SourceGenerator)
- [NuExt.Minimal.Behaviors.Wpf](https://github.com/nu-ext/NuExt.Minimal.Behaviors.Wpf)
- [NuExt.Presentation.Wpf](https://github.com/nu-ext/NuExt.Presentation.Wpf)
- [NuExt.Minimal.Mvvm.Wpf](https://github.com/nu-ext/NuExt.Minimal.Mvvm.Wpf)
- [NuExt.Minimal.Mvvm.MahApps.Metro](https://github.com/nu-ext/NuExt.Minimal.Mvvm.MahApps.Metro)

## Contributing

Issues and PRs are welcome. Keep changes minimal and performance-conscious.

## License

MIT. See LICENSE.