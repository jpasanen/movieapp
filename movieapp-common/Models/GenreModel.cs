using System.Text.Json.Serialization;

namespace movieapp_common.Models;

public class GenreModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
