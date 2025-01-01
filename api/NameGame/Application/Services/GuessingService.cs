using NameGame.Application.Queues.Interfaces;
using NameGame.Application.Services.Interfaces;
using NameGame.Models.Requests;
using NameGame.Websockets.Dispatchers;

namespace NameGame.Application.Services;

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
        Logger.LogInformation("Receiving a new guess. {user} guessed: {guess}", request.User, request.Guess);

        await GuessDispatcher.PublishGuessAsync(request, cancellationToken);

        await GuessQueue.EnqueueGuessAsync(request, cancellationToken);
    }
}