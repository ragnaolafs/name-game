using NameGame.Models.Requests;

namespace NameGame.Services;

public interface IGuessingService
{
    Task SubmitGuessAsync(GuessRequest guess, CancellationToken cancellationToken);
}