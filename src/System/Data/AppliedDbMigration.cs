namespace System.Data;

/// <summary>
/// Represents a database migration recorded as applied.
/// </summary>
public sealed record AppliedDbMigration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppliedDbMigration"/> class.
    /// </summary>
    /// <param name="id">The unique migration identifier.</param>
    /// <param name="appliedAt">The date and time when the migration was applied.</param>
    /// <param name="version">Optional version information associated with the applied migration.</param>
    public AppliedDbMigration(string id, DateTimeOffset appliedAt, string? version = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        Id = id;
        AppliedAt = appliedAt;
        Version = version;
    }

    /// <summary>
    /// Gets the unique migration identifier.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the date and time when the migration was applied.
    /// </summary>
    public DateTimeOffset AppliedAt { get; }

    /// <summary>
    /// Gets optional version information associated with the applied migration.
    /// </summary>
    public string? Version { get; }
}