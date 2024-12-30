using NameGame.Data.Queues;
using NameGame.Models.Requests;
using NameGame.Websockets.Dispatchers;

namespace NameGame.Services;

public class GuessingService(
    ILogger<GuessingService> logger,
    IGuessDispatcher guessDispatcher,
    IGuessQueue guessQueue)
    : IGuessingService
{
    private ILogger<GuessingService> Logger { get; } = logger;

    private IGuessDispatcher GuessDispatcher { get; } = guessDispatcher;

    private IGuessQueue GuessQueue { get; } = guessQueue;

    public async Task SubmitGuessAsync(
        GuessRequest request,
        CancellationToken cancellationToken)
    {
        this.Logger.LogInformation("Receiving a new guess. {user} guessed: {guess}", request.User, request.Guess);

        await this.GuessDispatcher.PublishGuessAsync(request, cancellationToken);

        await this.GuessQueue.EnqueueGuessAsync(request, cancellationToken);
    }
}