using System.Text.Json.Serialization;

namespace movieapp_common.Models;

public class PopularMoviesModel
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }

    [JsonPropertyName("results")]
    public List<PopularMovieModel> Movies { get; set; } = new List<PopularMovieModel>();
}
