
using NameGame.Data.Queues;

namespace NameGame.Services;

public class GameBackgroundService(
    ILogger<GameBackgroundService> logger,
    IGuessQueue guessQueue)
    : BackgroundService
{
    private ILogger<GameBackgroundService> Logger { get; } = logger;

    private IGuessQueue GuessQueue { get; } = guessQueue;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.Logger.LogInformation("Game Background Service is starting.");

        try
        {
            await foreach (var item in this.GuessQueue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    // Todo add DbContext here
                    this.Logger.LogInformation("Item stored in database. Guess: {guess}", item.Guess);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError(ex, "Error processing item: {Item}", item);
                }
            }
        }
        catch (OperationCanceledException)
        {
            Logger.LogInformation("Queue Processor Service is stopping.");
        }
    }
}