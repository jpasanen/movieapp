using System.ComponentModel.DataAnnotations;

namespace movieapp_common.Settings;

public class TheMovieDatabaseSettings
{
    /// <summary>
    /// Gets or sets the section key.
    /// </summary>
    public const string SectionKey = "TheMovieDatabase";

    /// <summary>
    /// Gets or sets the api url.
    /// </summary>
    [Required]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image url.
    /// </summary>
    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the api key.
    /// </summary>
    [Required]
    public string Key { get; set; } = string.Empty;    
}
