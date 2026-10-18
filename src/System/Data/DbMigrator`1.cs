namespace System.Data;

/// <summary>
/// Applies pending database migrations.
/// </summary>
/// <typeparam name="TContext">The type of the database migration context.</typeparam>
public sealed class DbMigrator<TContext> where TContext : DbMigrationContext
{
    private readonly IDbMigrationExecutor<TContext> _executor;
    private readonly IDbMigrationHistory<TContext> _history;
    private readonly DbMigration<TContext>[] _migrations;
    private readonly string? _version;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbMigrator{TContext}"/> class.
    /// </summary>
    /// <param name="executor">The database migration executor.</param>
    /// <param name="history">The database migration history.</param>
    /// <param name="migrations">The migrations in the order in which they must be applied.</param>
    /// <param name="version">Optional version information to record for each applied migration.</param>
    public DbMigrator(IDbMigrationExecutor<TContext> executor, IDbMigrationHistory<TContext> history, IEnumerable<DbMigration<TContext>> migrations, string? version = null)
    {
        ArgumentNullException.ThrowIfNull(executor);
        ArgumentNullException.ThrowIfNull(history);
        ArgumentNullException.ThrowIfNull(migrations);

        _executor = executor;
        _history = history;
        _migrations = [.. migrations];
        _version = version;

        ValidateMigrations(_migrations);
    }

    /// <summary>
    /// Applies all pending database migrations.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public void Migrate(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _executor.Execute(context => Migrate(context, cancellationToken), cancellationToken);
    }

    private void Migrate(TContext context, CancellationToken cancellationToken)
    {
        _history.EnsureCreated(context);

        var appliedMigrations = _history.GetAppliedMigrations(context);

        ValidateAppliedMigrations(appliedMigrations);

        for (var i = appliedMigrations.Count; i < _migrations.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var migration = _migrations[i];

            migration.Up(context);

            _history.Add(context, new AppliedDbMigration(i + 1, migration.Id, DateTimeOffset.UtcNow, _version));
        }
    }

    private static void ValidateMigrations(DbMigration<TContext>[] migrations)
    {
        var migrationIds = new HashSet<string>(StringComparer.Ordinal);

        for (var i = 0; i < migrations.Length; i++)
        {
            var migration = migrations[i] ?? throw new DbMigrationException($"The migration at index {i} is null.");
            if (string.IsNullOrWhiteSpace(migration.Id))
            {
                throw new DbMigrationException($"The migration at index {i} has an empty identifier.");
            }

            if (!migrationIds.Add(migration.Id))
            {
                throw new DbMigrationException($"Duplicate migration identifier '{migration.Id}'.");
            }
        }
    }

    private void ValidateAppliedMigrations(IReadOnlyList<AppliedDbMigration> appliedMigrations)
    {
        ArgumentNullException.ThrowIfNull(appliedMigrations);

        if (appliedMigrations.Count > _migrations.Length)
        {
            throw new DbMigrationException("The database contains migrations that are not present in the current migration configuration.");
        }

        for (var i = 0; i < appliedMigrations.Count; i++)
        {
            var appliedMigration = appliedMigrations[i];
            var configuredMigration = _migrations[i];
            var expectedSequence = i + 1;

            if (appliedMigration.Sequence != expectedSequence)
            {
                throw new DbMigrationException(
                    $"The applied migration '{appliedMigration.Id}' has sequence {appliedMigration.Sequence}, " +
                    $"but sequence {expectedSequence} was expected.");
            }

            if (!StringComparer.Ordinal.Equals(appliedMigration.Id, configuredMigration.Id))
            {
                throw new DbMigrationException(
                    $"The applied migration '{appliedMigration.Id}' at sequence {appliedMigration.Sequence} does not match " +
                    $"the configured migration '{configuredMigration.Id}'.");
            }
        }
    }
}