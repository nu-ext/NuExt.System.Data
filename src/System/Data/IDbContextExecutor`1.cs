namespace System.Data;

/// <summary>
/// Executes database operations within an owned database context.
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
public interface IDbContextExecutor<TDbContext> where TDbContext : IDbContext
{
    /// <summary>
    /// Executes the specified action within an owned database context.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the operation.</returns>
    ValueTask ExecuteAsync(Action<TDbContext> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified asynchronous action within an owned database context.
    /// </summary>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask ExecuteAsync(Func<TDbContext, ValueTask> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified function within an owned database context.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that contains the result of the operation.</returns>
    ValueTask<T> ExecuteAsync<T>(Func<TDbContext, T> func, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified asynchronous function within an owned database context.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that contains the result of the asynchronous operation.</returns>
    ValueTask<T> ExecuteAsync<T>(Func<TDbContext, ValueTask<T>> func, CancellationToken cancellationToken = default);
}