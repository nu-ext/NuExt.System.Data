using System.Diagnostics;

namespace System.Data;

/// <summary>
/// Provides extension methods for database contexts.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Attempts to roll back the active transaction.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="originalException">The exception that caused the rollback attempt, if any.</param>
    /// <returns>
    /// <see langword="true"/> if the transaction was rolled back;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null"/>.</exception>
    public static bool TryRollback(this IDbContext context, Exception? originalException = null)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (!context.IsActive)
        {
            return false;
        }

        try
        {
            context.Rollback();
            return true;
        }
        catch (Exception rollbackException)
        {
            string message = originalException is not null
                ? $"Database transaction rollback failed after '{originalException.GetType()}': {rollbackException}"
                : $"Database transaction rollback failed: {rollbackException}";

            Trace.WriteLine(message);
            Debug.Fail(message);
            return false;
        }
    }
}