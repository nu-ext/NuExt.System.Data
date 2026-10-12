namespace System.Data;

/// <summary>
/// Provides access to the database migration history.
/// </summary>
/// <typeparam name="TContext">The type of the database migration context.</typeparam>
public interface IDbMigrationHistory<TContext> where TContext : DbMigrationContext
{
    /// <summary>
    /// Ensures that the migration history storage exists.
    /// </summary>
    /// <param name="context">The database migration context.</param>
    void EnsureCreated(TContext context);

    /// <summary>
    /// Gets the migrations recorded as applied.
    /// </summary>
    /// <param name="context">The database migration context.</param>
    /// <returns>The applied migrations in the order in which they were applied.</returns>
    IReadOnlyList<AppliedDbMigration> GetAppliedMigrations(TContext context);

    /// <summary>
    /// Adds an applied migration to the migration history.
    /// </summary>
    /// <param name="context">The database migration context.</param>
    /// <param name="migration">The applied migration.</param>
    void Add(TContext context, AppliedDbMigration migration);
}