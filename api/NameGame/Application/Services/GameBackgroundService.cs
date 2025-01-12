using Microsoft.EntityFrameworkCore;
using NameGame.Application.Queues.Interfaces;
using NameGame.Application.Score;
using NameGame.Data.Contexts;
using NameGame.Exceptions;
using NameGame.Models;
using NameGame.Models.Requests;
using NameGame.Websockets.Dispatchers;

namespace NameGame.Application.Services;

public class GameBackgroundService(
    ILogger<GameBackgroundService> logger,
    IGuessQueue guessQueue,
    IGuessDispatcher guessDispatcher,
    IDbContextFactory<NameGameDbContext> dbContextFactory)
    : BackgroundService
{
    private ILogger<GameBackgroundService> Logger { get; } = logger;

    private IGuessQueue GuessQueue { get; } = guessQueue;

    private IGuessDispatcher GuessDispatcher { get; } = guessDispatcher;

    private NameGameDbContext DbContext { get; } = dbContextFactory.CreateDbContext();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Game Background Service is starting.");

        try
        {
            await foreach (var item in GuessQueue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await this.HandleGuessAsync(item, stoppingToken);

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

    private async Task HandleGuessAsync(
        AddGuessInput input,
        CancellationToken cancellationToken)
    {
        var game = await this.DbContext.Games
            .FirstOrDefaultAsync(g => g.Id == input.GameId, cancellationToken)
            ?? throw new GameNotFoundException(input.GameId);

        var score = GuessScoreCalculator.CalculateScore(input.Guess, game.Answer);

        this.DbContext.Guesses.Add(new GuessEntity
        {
            GameId = game.Id,
            User = input.User,
            Guess = input.Guess,
            Score = score
        });

        await this.DbContext.SaveChangesAsync(cancellationToken);

        await this.GuessDispatcher.PublishGuessAsync(input, cancellationToken);
    }
}