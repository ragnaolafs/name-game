namespace NameGame.Models.Results;

public record GuessResult(
    string Id,
    string GameId,
    string User,
    string Guess,
    double Score);