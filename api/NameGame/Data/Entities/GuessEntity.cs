using NameGame.Data.Entities;
using NameGame.Data.Interfaces;

namespace NameGame.Models;

public class GuessEntity : ITimeStamps
{
    public string Id { get; set; } = default!;

    public required string GameId { get; set; }

    public required string User { get; set; }

    public required string Guess { get; set; }

    /// <summary>
    /// Score from 0 to 1
    /// </summary>
    public double Score { get; set; }

    public List<int>? HintIndicesJson { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public GameEntity? Game { get; set; }
}