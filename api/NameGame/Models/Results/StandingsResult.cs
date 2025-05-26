namespace NameGame.Models.Results;

public record StandingsResult(
    string GameId,
    List<GuessResult> TopGuesses,
    DateTime Timestamp);