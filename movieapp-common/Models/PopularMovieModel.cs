using System.Text.Json.Serialization;

namespace movieapp_common.Models;

public class PopularMovieModel
{
    private string _posterPath = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; set; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string PosterPath
    {
        get
        {
            return $"https://image.tmdb.org/t/p/w200{_posterPath}";
        }
        set
        {
            _posterPath = value;
        }
    }
    [JsonPropertyName("release_date")]
    public DateOnly ReleaseDate { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}

