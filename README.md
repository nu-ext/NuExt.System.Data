# NuExt.System.Data

`NuExt.System.Data` provides high-level data access utilities for .NET, including extensions for `DataReader`, `DataRow`, and `DataTable`, lightweight DAL infrastructure, database context abstractions, and a provider-independent migration framework.

[![NuGet](https://img.shields.io/nuget/v/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)
[![Ask DeepWiki](https://img.shields.io/badge/Ask-DeepWiki-blue)](https://deepwiki.com/nu-ext/NuExt.System.Data)
[![Build](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml/badge.svg)](https://github.com/nu-ext/NuExt.System.Data/actions/workflows/ci.yml)
[![License](https://img.shields.io/github/license/nu-ext/NuExt.System.Data?label=license)](https://github.com/nu-ext/NuExt.System.Data/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/NuExt.System.Data.svg)](https://www.nuget.org/packages/NuExt.System.Data)

### Features

- **ADO.NET Extensions**: Provides helpers for reading, converting, and manipulating data through `DataReader`, `DataRow`, and `DataTable`.
- **DAL Infrastructure**: Includes lightweight base classes and database context abstractions for implementing data access layers.
- **Database Migrations**: Provides provider-independent migration orchestration with ordered execution, validated migration history, optional version metadata, cancellation support, and provider-specific execution contexts.

### Commonly Used Types

- **`System.Data.DalBase<TDbContext>`**: Base class for Data Access Layer (DAL) implementations.
- **`System.Data.DataReaderExtensions`**: Provides extension methods for data reader operations.
- **`System.Data.DataRowExtensions`**: Provides extension methods for data row operations.
- **`System.Data.DataTableExtensions`**: Provides extension methods for data table operations.
- **`System.Data.DbMigration<TContext>`**: Base class for defining ordered database schema and data migrations.
- **`System.Data.DbMigrationContext`**: Base class for provider-specific migration execution contexts.
- **`System.Data.DbMigrator<TContext>`**: Validates migration configuration and history, and applies pending migrations.
- **`System.Data.IDbMigrationExecutor<TContext>`**: Defines provider-specific migration execution and 

### Installation

Via [NuGet](https://www.nuget.org/):

```sh
dotnet add package NuExt.System.Data
```

Or via Visual Studio:

1. Go to `Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution...`.
2. Search for `NuExt.System.Data`.
3. Click "Install".

### Usage Examples

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