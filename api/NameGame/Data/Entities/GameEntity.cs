using System.Text.Json.Serialization;
using NameGame.Data.Interfaces;
using NameGame.Models.Enums;

namespace NameGame.Data.Entities;

public class GameEntity : ITimeStamps
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("status")]
    public GameStatus Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}