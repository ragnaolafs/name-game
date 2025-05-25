using System.Text.Json.Serialization;

namespace NameGame.Models.Requests;

public class AddGuessRequest
{
    [JsonPropertyName("user")]
    public string User { get; set; } = default!;

    [JsonPropertyName("guess")]
    public string Guess { get; set; } = default!;

    // todo add if guess was randomized
}