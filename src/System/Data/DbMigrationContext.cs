namespace System.Data;

/// <summary>
/// Provides the execution context for a database migration.
/// </summary>
public abstract class DbMigrationContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DbMigrationContext"/> class.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    protected DbMigrationContext(CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;
    }

    /// <summary>
    /// Gets a token to monitor for cancellation requests.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    public void ThrowIfCancellationRequested()
    {
        CancellationToken.ThrowIfCancellationRequested();
    }
}