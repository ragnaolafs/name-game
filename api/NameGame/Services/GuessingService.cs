using NameGame.Models.Requests;
using NameGame.Websockets.Dispatchers;

namespace NameGame.Services;

public class GuessingService(
    ILogger<GuessingService> logger,
    IGuessDispatcher guessDispatcher)
    : IGuessingService
{
    private ILogger<GuessingService> Logger { get; } = logger;

    private IGuessDispatcher Dispatcher { get; } = guessDispatcher;

    public async Task SubmitGuessAsync(
        GuessRequest request,
        CancellationToken cancellationToken)
    {
        this.Logger.LogInformation("Receiving a new guess. {user} guessed: {guess}", request.User, request.Guess);

        await this.Dispatcher.PublishGuessAsync(request, cancellationToken);
    }
}