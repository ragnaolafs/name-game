using NameGame.Models.Requests;

namespace NameGame.Application.Services.Interfaces;

public interface IGuessingService
{
    Task SubmitGuessAsync(GuessRequest guess, CancellationToken cancellationToken);
}