namespace System.Data;

/// <summary>
/// Represents an error caused by an invalid database migration configuration or history.
/// </summary>
public sealed class DbMigrationException : DataException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DbMigrationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DbMigrationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbMigrationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public DbMigrationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}