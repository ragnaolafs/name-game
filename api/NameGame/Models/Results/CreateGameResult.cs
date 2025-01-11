using NameGame.Models.Enums;

namespace NameGame.Models.Results;

public record CreateGameResult(string Id, GameStatus Status);