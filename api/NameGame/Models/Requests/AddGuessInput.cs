namespace NameGame.Models.Requests;

public record AddGuessInput(
    string GameId,
    string User,
    string Guess);