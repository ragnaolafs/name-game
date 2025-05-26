using NameGame.Models.Enums;

namespace NameGame.Models.Results;

public record GetGameResult(
    string Id,
    GameStatus Status,
    string? Handle,
    StandingsResult Standings);