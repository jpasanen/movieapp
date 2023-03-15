namespace movieapp_common.Settings;

/// <summary>
/// Class for blob service configuration.
/// </summary>
public class BlobServiceSettings
{
    /// <summary>
    /// Gets or sets section key.
    /// </summary>
    public const string SectionKey = "BlobServiceSettings";

    /// <summary>
    /// Gets or sets the connectionstring property.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}
