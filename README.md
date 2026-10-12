# NuExt.System.Data

`NuExt.System.Data` provides a lightweight and extensible foundation for data access in .NET applications. It combines practical ADO.NET extensions, reusable DAL and database context abstractions, explicit transaction management, and provider-independent database migrations with validated history tracking.

[![NuGet](https://img.shields.io/nuget/v/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)
[![Ask DeepWiki](https://img.shields.io/badge/Ask-DeepWiki-blue)](https://deepwiki.com/nu-ext/NuExt.System.Data)
[![Build](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml/badge.svg)](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml)
[![License](https://img.shields.io/github/license/nu-ext/NuExt.System.Data?label=license)](https://github.com/nu-ext/NuExt.System.Data/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)

## Features

- **ADO.NET Extensions**: Provides helpers for reading, converting, and manipulating data through `DataReader`, `DataRow`, and `DataTable`.
- **DAL Infrastructure**: Includes lightweight base classes and database context abstractions for implementing data access layers.
- **Database Migrations**: Provides provider-independent migration orchestration with ordered execution, validated migration history, optional version metadata, cancellation support, and provider-specific execution contexts.

## DAL Context Ownership

The DAL infrastructure supports two execution models:

- **Owned context**: When no database context is provided, the provider-specific executor creates and owns the context. It manages the connection, transaction, commit or rollback, and resource cleanup.
- **Borrowed context**: When an existing database context is provided, the DAL participates in the caller's unit of work without committing, rolling back, or disposing the context.

This allows individual DAL operations to run independently while also supporting coordinated operations across multiple DAL components within the same transaction.

## Database Migration Model

The migration framework is designed for application-integrated, forward-only database evolution. Migrations are registered explicitly in the order in which they must be applied, making the target schema deterministic and avoiding reflection-based discovery.

Applied migrations are stored as an ordered history. Before applying pending migrations, the framework validates that the stored history is an exact prefix of the configured migration sequence. This detects removed, renamed, reordered, or unknown migrations before any schema changes are made.

Transaction ownership is explicit and provider-controlled. A provider can execute the complete migration batch within its own transaction or participate in an existing database context, allowing migrations and application operations to share the same transaction.

## Commonly Used Types

- **`System.Data.DalBase<TDbContext>`**: Base class for DAL implementations that supports borrowed contexts and provider-managed owned context scopes.
- **`System.Data.DataReaderExtensions`**: Provides extension methods for data reader operations.
- **`System.Data.DataRowExtensions`**: Provides extension methods for data row operations.
- **`System.Data.DataTableExtensions`**: Provides extension methods for data table operations.
- **`System.Data.DbMigration<TContext>`**: Base class for defining ordered database schema and data migrations.
- **`System.Data.DbMigrationContext`**: Base class for provider-specific migration execution contexts.
- **`System.Data.DbMigrator<TContext>`**: Validates migration configuration and applied history, and executes pending migrations.
- **`System.Data.IDbContext`**: Defines transaction state and lifecycle operations for database contexts.
- **`System.Data.IDbContextExecutor<TDbContext>`**: Defines provider-specific ownership and execution of database context scopes.
- **`System.Data.IDbMigrationExecutor<TContext>`**: Defines provider-specific migration execution and transaction handling.
- **`System.Data.IDbMigrationHistory<TContext>`**: Defines storage operations for applied migration history.

## Installation

Via [NuGet](https://www.nuget.org/):

```sh
dotnet add package NuExt.System.Data
```

Or via Visual Studio:

1. Go to `Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution...`.
2. Search for `NuExt.System.Data`.
3. Click "Install".

### Compatibility

**.NET Standard 2.0+, .NET 8/9/10 and .NET Framework 4.6.2+**

## Usage Examples

For a complete provider implementation and migration example, see:

- [NuExt.System.Data.SQLite](https://github.com/nu-ext/NuExt.System.Data.SQLite)

## Ecosystem

- [NuExt.Minimal.Mvvm](https://github.com/nu-ext/NuExt.Minimal.Mvvm)
- [NuExt.Minimal.Mvvm.SourceGenerator](https://github.com/nu-ext/NuExt.Minimal.Mvvm.SourceGenerator)
- [NuExt.Minimal.Behaviors.Wpf](https://github.com/nu-ext/NuExt.Minimal.Behaviors.Wpf)
- [NuExt.Minimal.Mvvm.Wpf](https://github.com/nu-ext/NuExt.Minimal.Mvvm.Wpf)
- [NuExt.Minimal.Mvvm.MahApps.Metro](https://github.com/nu-ext/NuExt.Minimal.Mvvm.MahApps.Metro)
- [NuExt.System](https://github.com/nu-ext/NuExt.System)
- [NuExt.System.Data.SQLite](https://github.com/nu-ext/NuExt.System.Data.SQLite)

## Contributing

Issues and PRs are welcome. Keep changes minimal and performance-conscious.

## License

MIT. See LICENSE.