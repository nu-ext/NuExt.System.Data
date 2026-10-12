namespace System.Data;

/// <summary>
/// Represents a database migration that applies a single schema or data change.
/// </summary>
/// <typeparam name="TContext">The type of the database migration context.</typeparam>
public abstract class DbMigration<TContext> where TContext : DbMigrationContext
{
    /// <summary>
    /// Gets the unique identifier of this migration.
    /// </summary>
    public abstract string Id { get; }

    /// <summary>
    /// Applies the migration.
    /// </summary>
    /// <param name="context">The database context.</param>
    public abstract void Up(TContext context);
}