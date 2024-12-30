using NameGame.Data.Interfaces;

namespace NameGame.Models;

public class GuessEntity : ITimeStamps
{
    public required string GameId { get; set; }

    public required string User { get; set; }

    public required string Guess { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}