using Microsoft.EntityFrameworkCore;

namespace movieapp_backend.Features.Movies;

/// <summary>
/// A class for the <see cref="MovieEntity"/>.
/// </summary>
[Keyless]
public class MovieEntity
{
    public bool Adult { get; set; }    

    public string? Homepage { get; set; } = string.Empty;

    public int Id { get; set; }

    public string ImdbId { get; set; } = string.Empty;

    public string OriginalLanguage { get; set; } = string.Empty;

    public string OriginalTitle { get; set; } = string.Empty;

    public string Overview { get; set; } = string.Empty;

    public string PosterPath { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public List<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
}
