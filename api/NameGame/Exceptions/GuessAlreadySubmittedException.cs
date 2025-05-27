namespace NameGame.Exceptions;

public class GuessAlreadySubmittedException(string gameId, string guess) :
    Exception($"Guess '{guess}' has already been submitted for game: {gameId}")
{
    public string GameId { get; } = gameId;

    public string Guess { get; } = guess;
}