using System.Text.Json.Serialization;

namespace NameGame.Models.Requests;

public class AddGuessRequest
{
    [JsonPropertyName("user")]
    public required string User { get; set; }

    [JsonPropertyName("guess")]
    public required string Guess { get; set; }

    // todo add if guess was randomized
}