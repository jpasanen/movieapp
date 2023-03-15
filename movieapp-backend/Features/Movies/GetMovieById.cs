using MediatR;
using Microsoft.EntityFrameworkCore;

namespace movieapp_backend.Features.Movies;

internal class GetMovieById : IRequestHandler<GetMovieById.Query, MovieEntity?>
{
    private readonly MovieDbContext _movieDbContext;

    public GetMovieById(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    public async Task<MovieEntity?> Handle(Query request, CancellationToken cancellationToken)
    {
        return await _movieDbContext.Movies
            .FirstOrDefaultAsync(x => x.Id == request.id);
    }

    internal record struct Query(int id) : IRequest<MovieEntity>;
}
