using NameGame.Models.Enums;

namespace NameGame.Models.Results;

public record CreateGameResult(
    string Id,
    string? Handle,
    GameStatus Status);