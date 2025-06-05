using NameGame.Models.Enums;

namespace NameGame.Models.Results;

public record GameStatusResult(
    string GameId,
    GameStatus Status,
    WinnerResult? Winner = null);