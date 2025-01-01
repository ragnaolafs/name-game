using NameGame.Application.Queues.Interfaces;

namespace NameGame.Application.Services;

public class GameBackgroundService(
    ILogger<GameBackgroundService> logger,
    IGuessQueue guessQueue)
    : BackgroundService
{
    private ILogger<GameBackgroundService> Logger { get; } = logger;

    private IGuessQueue GuessQueue { get; } = guessQueue;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Game Background Service is starting.");

        try
        {
            await foreach (var item in GuessQueue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    // Todo add DbContext here
                    Logger.LogInformation("Item stored in database. Guess: {guess}", item.Guess);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error processing item: {Item}", item);
                }
            }
        }
        catch (OperationCanceledException)
        {
            Logger.LogInformation("Queue Processor Service is stopping.");
        }
    }
}