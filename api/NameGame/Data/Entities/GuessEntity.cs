using NameGame.Data.Entities;
using NameGame.Data.Interfaces;

namespace NameGame.Models;

public class GuessEntity : ITimeStamps
{
    public string Id { get; set; } = default!;

    public required string GameId { get; set; }

    public required string User { get; set; }

    public required string Guess { get; set; }

    public double Score { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public GameEntity? Game { get; set; }
}