using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using movieapp_common.Models;
using movieapp_common.Settings;
using System.Net.Http.Json;

namespace movieapp_common.Services;

/// <summary>
/// Class for the movie database api integration.
/// </summary>
public class TheMovieDatabaseApi
{
    private readonly ILogger<TheMovieDatabaseApi> _logger;
    private readonly IOptionsMonitor<TheMovieDatabaseSettings> _options;
    private static HttpClient? _httpClient;

    public TheMovieDatabaseApi(IOptionsMonitor<TheMovieDatabaseSettings> options,
        ILoggerFactory loggerFactory) 
    {
        _options = options;
        _logger = loggerFactory.CreateLogger<TheMovieDatabaseApi>();

        var handler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(options.CurrentValue.Url)
        };
    }

    public async Task<PopularMoviesModel?> GetAllAsync(int page = 1)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<PopularMoviesModel>($"/3/movie/popular?api_key={_options.CurrentValue.Key}&page={page}")
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch popular movie list: {ex.Message}");
            return null;
        }
    }

    public async Task<MovieModel?> GetByIdAsync(int id)
    {
        try
        {
            return await _httpClient
                .GetFromJsonAsync<MovieModel>($"/3/movie/{id}?api_key={_options.CurrentValue.Key}")
                .ConfigureAwait(false);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch movie details: {ex.Message}");
            return null;
        }
    }
}
