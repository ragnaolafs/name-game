using System.Text.Json.Serialization;
using NameGame.Models.Enums;

namespace NameGame.Models;

public class Game
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("status")]
    public GameStatus Status { get; set; }
}