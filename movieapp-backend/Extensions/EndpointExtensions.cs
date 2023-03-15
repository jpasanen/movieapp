using MediatR;
using movieapp_backend.Features.Movies;

namespace movieapp_backend.Extensions;

/// <summary>
/// Class for endpoint extensions and MediatR requests.
/// </summary>
public static class EndpointExtensions
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        app.MapGet("/movies/{id}", async (IMediator mediator, int id) =>
        {
            return await GetByIdAsync(mediator, id);
        })
        .WithOpenApi();
        
        app.MapGet("/movies/theater/{id}", async (IMediator mediator, int id) =>
        {
            return await GetAllAsync(mediator, id);
        })
        .WithOpenApi();
    }

    private static async Task<IResult> GetByIdAsync(IMediator mediator, int id)
    {
        var movie = await mediator.Send(new GetMovieById.Query(id));

        if (movie != null)
        {
            return Results.Ok(movie);
        }
        else
        {
            return Results.NotFound();
        }
    }

    private static async Task<IResult> GetAllAsync(IMediator mediator, int id)
    {
        var movies = await mediator.Send(new GetAllMovies.Query(id));

        if (movies != null)
        {
            return Results.Ok(movies);
        }
        else
        {
            return Results.NotFound();
        }
    }
}
