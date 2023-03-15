using movieapp_backend.Features.Movies;
using movieapp_common.Models;

namespace movieapp_common.Extensions;

/// <summary>
/// Movies extension class for data conversions.
/// </summary>
public static class MovieExtensions
{
    public static MovieModel ToMovieModel(this MovieEntity model)
    {
        return new MovieModel
        {
            Id = model.Id,
            OriginalLanguage = model.OriginalLanguage,
            OriginalTitle = model.OriginalTitle,
            Overview = model.Overview,
            PosterPath = model.PosterPath,
            ReleaseDate = DateOnly.FromDateTime(model.ReleaseDate),
            Title = model.Title,
            Adult = model.Adult,
            Genres = model.Genres.Select(x => new GenreModel 
            { 
                Id = x.GenreId,
                Name = x.GenreName,
            }).ToArray(),
            Homepage = model.Homepage ?? string.Empty,
            ImdbId = model.ImdbId,
            Status = model.Status,            
        };
    }
}
