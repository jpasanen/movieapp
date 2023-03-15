namespace movieapp_common.Settings;

/// <summary>
/// Class for movie database configuration.
/// </summary>
public class MovieDatabaseSettings
{
    /// <summary>
    /// Gets or sets section key.
    /// </summary>
    public const string SectionKey = "Database";

    /// <summary>
    /// Gets or sets the connectionstring property.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}
