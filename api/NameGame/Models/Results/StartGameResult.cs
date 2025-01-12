using NameGame.Models.Enums;

namespace NameGame.Models.Results;

public record StartGameResult(
    string Id,
    string? Handle,
    GameStatus Status);