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

    /// <summary>
    /// Throws an <see cref="OperationCanceledException"/> if cancellation has been requested.
    /// </summary>
    public void ThrowIfCancellationRequested()
    {
        CancellationToken.ThrowIfCancellationRequested();
    }
}