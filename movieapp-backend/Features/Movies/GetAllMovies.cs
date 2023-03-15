using MediatR;
using Microsoft.EntityFrameworkCore;
using movieapp_common.Extensions;
using movieapp_common.Models;

namespace movieapp_backend.Features.Movies;

internal class GetAllMovies : IRequestHandler<GetAllMovies.Query, List<MovieModel>>
{
    private const int MoviesPerDay = 10;
    private readonly MovieDbContext _movieDbContext;
    private List<Tuple<TimeOnly, int>> _schedule = new List<Tuple<TimeOnly, int>>();

    public GetAllMovies(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    public async Task<List<MovieModel>> Handle(Query request, CancellationToken cancellationToken)
    {
        InitializeSchedule();
        int i = 0;

        var dbResult = await _movieDbContext.Movies
            .Include(x => x.Genres)
            .ToListAsync(cancellationToken);
        
        var result = new List<MovieModel>();

        if (dbResult != null)
        {
            foreach (var movie in dbResult.Take(MoviesPerDay))
            {
                var fullMovie = movie.ToMovieModel();

                fullMovie.Date = DateOnly.FromDateTime(DateTime.Now);
                fullMovie.Time = _schedule.ElementAt(i).Item1;
                fullMovie.Theater = _schedule.ElementAt(i).Item2;

                result.Add(fullMovie);
                i++;
            }
        }
        return result;
    }

    private void InitializeSchedule()
    {
        var theater = 1;
        var startDate = DateTime.Now.Date.AddHours(13);
        var endDate = DateTime.Now.Date.AddHours(22);

        for (var i = 1;i <= MoviesPerDay;i++)
        {
            var rand = new Random();
            var halfHours = rand.Next((22 - 13) * 2);

            var newDateTime = startDate.AddMinutes(halfHours * 30);
            _schedule.Add(Tuple.Create(TimeOnly.FromDateTime(newDateTime), theater));

            if (i % 5 == 0)
            {
                theater++;
            }
        }
    }

    internal record struct Query(int id) : IRequest<List<MovieModel>>;
}