using Microsoft.EntityFrameworkCore;

namespace movieapp_backend.Features.Movies;

/// <summary>
/// A class for the <see cref="GenreEntity"/>.
/// </summary>
[Keyless]
public class GenreEntity
{
    public int Id { get; set; }

    public int GenreId { get; set; }

    public string GenreName { get; set; } = string.Empty;

    public MovieEntity Movie { get; set; } = new MovieEntity();
}
