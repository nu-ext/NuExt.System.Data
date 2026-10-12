using System.ComponentModel;

namespace System.Data;

/// <summary>
/// Provides database context execution for Data Access Layer operations.
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
public abstract class DalBase<TDbContext> : Disposable where TDbContext : IDbContext
{
    private readonly IDbContextExecutor<TDbContext> _executor;

    /// <summary>
    /// Initializes a new instance of the <see cref="DalBase{TDbContext}"/> class.
    /// </summary>
    /// <param name="executor">The database context executor.</param>
    /// <exception cref="ArgumentNullException"><paramref name="executor"/> is <see langword="null"/>.</exception>
    protected DalBase(IDbContextExecutor<TDbContext> executor)
    {
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));
    }

    #region Methods

    /// <summary>
    /// Executes the specified action using an existing database context or an owned context created by the executor.
    /// </summary>
    /// <param name="context">An optional existing database context.</param>
    /// <param name="action">The action to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the operation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    protected ValueTask ExecuteInDbContextAsync(TDbContext? context, Action<TDbContext> action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        cancellationToken.ThrowIfCancellationRequested();
        CheckDisposed();

        if (context is null)
        {
            return _executor.ExecuteAsync(action, cancellationToken);
        }

        action(context);
        return default;
    }

    /// <summary>
    /// Executes the specified asynchronous action using an existing database context or an owned context created by the executor.
    /// </summary>
    /// <param name="context">An optional existing database context.</param>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    protected ValueTask ExecuteInDbContextAsync(TDbContext? context, Func<TDbContext, ValueTask> action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        cancellationToken.ThrowIfCancellationRequested();
        CheckDisposed();

        return context is null ? _executor.ExecuteAsync(action, cancellationToken) : action(context);
    }

    /// <summary>
    /// Executes the specified function using an existing database context or an owned context created by the executor.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="context">An optional existing database context.</param>
    /// <param name="func">The function to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task containing the result of the operation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
    protected ValueTask<T> ExecuteInDbContextAsync<T>(TDbContext? context, Func<TDbContext, T> func, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(func);
        cancellationToken.ThrowIfCancellationRequested();
        CheckDisposed();

        return context is null ? _executor.ExecuteAsync(func, cancellationToken) : new ValueTask<T>(func(context));
    }

    /// <summary>
    /// Executes the specified asynchronous function using an existing database context or an owned context created by the executor.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="context">An optional existing database context.</param>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task containing the result of the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
    protected ValueTask<T> ExecuteInDbContextAsync<T>(TDbContext? context, Func<TDbContext, ValueTask<T>> func, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(func);
        cancellationToken.ThrowIfCancellationRequested();
        CheckDisposed();

        return context is null ? _executor.ExecuteAsync(func, cancellationToken) : func(context);
    }

    #endregion
}