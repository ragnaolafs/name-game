using NameGame.Data.Interfaces;
using NameGame.Models;
using NameGame.Models.Enums;

namespace NameGame.Data.Entities;

public class GameEntity : ITimeStamps
{
    public string Id { get; set; } = default!;

    public string? Handle { get; set; }

    public GameStatus Status { get; set; }

    public required string Answer { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<GuessEntity>? Guesses { get; set; }
}