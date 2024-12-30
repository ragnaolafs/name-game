using System.Text.Json.Serialization;

namespace NameGame.Models.Requests;

public class GuessRequest
{
    [JsonPropertyName("user")]
    public required string User { get; set; }

    [JsonPropertyName("guess")]
    public required string Guess { get; set; }
}