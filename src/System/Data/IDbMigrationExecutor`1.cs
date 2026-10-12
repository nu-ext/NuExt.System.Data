namespace System.Data;

/// <summary>
/// Executes database migration operations within a provider-specific scope.
/// </summary>
/// <typeparam name="TContext">The type of the database migration context.</typeparam>
public interface IDbMigrationExecutor<TContext> where TContext : DbMigrationContext
{
    /// <summary>
    /// Executes the specified operation within a database migration scope.
    /// </summary>
    /// <param name="operation">The operation to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    void Execute(Action<TContext> operation, CancellationToken cancellationToken);
}