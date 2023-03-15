using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using movieapp_common.Models;
using movieapp_common.Services;
using movieapp_common.Settings;
using Newtonsoft.Json;

namespace movieapp_function.Functions;

/// <summary>
/// Function for importing movies to database. Scheduled daily at 8AM.
/// </summary>
public class DataImportFunction
{
    private readonly ILogger _logger;
    private readonly TheMovieDatabaseApi _theMovieDatabaseApi;
    private readonly MovieStorageService _movieStorageService;
    private readonly IOptionsMonitor<TheMovieDatabaseSettings> _options;

    public DataImportFunction(ILoggerFactory loggerFactory, 
        TheMovieDatabaseApi theMovieDatabaseApi,
        MovieStorageService movieStorageService,
        IOptionsMonitor<TheMovieDatabaseSettings> options)
    {
        _logger = loggerFactory.CreateLogger<DataImportFunction>();
        _theMovieDatabaseApi = theMovieDatabaseApi;
        _movieStorageService = movieStorageService;
        _options = options;
    }

    [Function("DataImportFunction")]
    public async Task Run([TimerTrigger("0 0 8 * * *", RunOnStartup = true)] FunctionContext context)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        var movies = await _theMovieDatabaseApi.GetAllAsync();
        if (movies == null )
        {
            _logger.LogError($"Failed to fetch movies");
            return;
        }

        var fullMoviesList = new List<MovieModel>();
        foreach (var movie in movies.Movies)
        {
            var fullMovie = await _theMovieDatabaseApi.GetByIdAsync(movie.Id);
            if (fullMovie != null)
            {
                // Generate the full url to poster image.
                fullMovie.PosterPath = $"{_options.CurrentValue.ImageUrl}{fullMovie.PosterPath}";

                fullMoviesList.Add(fullMovie);
            }
            else
            {
                _logger.LogError($"Failed to fetch movie {movie.Id}!");
            }
        }

        var moviesSerialized = JsonConvert.SerializeObject(fullMoviesList);

        var storageResult = await _movieStorageService.SaveAsync(moviesSerialized);
        if (!storageResult)
        {
            _logger.LogError($"Failed to save serialized movies!");
        }
    }
}
