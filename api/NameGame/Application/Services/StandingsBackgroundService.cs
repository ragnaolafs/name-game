using Microsoft.EntityFrameworkCore;
using NameGame.Application.Queues.Interfaces;
using NameGame.Data.Contexts;
using NameGame.Data.Extensions;
using NameGame.Models.Results;
using NameGame.Websockets.Dispatchers.Interfaces;

namespace NameGame.Application.Services;

public class StandingsBackgroundService(
    ILogger<StandingsBackgroundService> logger,
    IStandingsQueue standingsQueue,
    IStandingsDispatcher standingsDispatcher,
    IDbContextFactory<NameGameDbContext> dbContextFactory,
    IConfiguration configuration)
    : BackgroundService
{
    private ILogger<StandingsBackgroundService> Logger { get; } = logger;

    private IStandingsQueue StandingsQueue { get; } = standingsQueue;

    private IStandingsDispatcher StandingsDispatcher { get; } = standingsDispatcher;

    private NameGameDbContext DbContext { get; } = dbContextFactory.CreateDbContext();

    private Dictionary<string, StandingsResult> Standings { get; } = [];

    private int TopPlayersLimit { get; }
        = configuration.GetValue<int?>("TopPlayersLimit") ?? 10;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.Logger.LogInformation("Standings background service is starting.");

        try
        {
            await foreach (var item in this.StandingsQueue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await this.UpdateStandingsAsync(item, stoppingToken);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError(ex, "Error processing update-standings item.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            this.Logger.LogInformation("Standings background service is stopping");
        }
    }

    private async Task UpdateStandingsAsync(
        GuessResult newGuess,
        CancellationToken cancellationToken)
    {
        this.Logger.LogDebug(
            "calculating standings. top player limit is {limit}",
            this.TopPlayersLimit);

        if (!this.Standings.TryGetValue(newGuess.GameId, out var currentStandings))
        {
            currentStandings = null;
        }

        var lowestScore = currentStandings?.TopGuesses.LastOrDefault()?.Score;

        if (newGuess.Score < lowestScore)
        {
            this.Logger.LogInformation(
                "New guess score {score} is lower than the lowest score in current standings {lowestScore}. No need to update standings.",
                newGuess.Score,
                lowestScore);
            // return;
        }

        var newStandings = await this.DbContext.Guesses.CalculateStandings(
            newGuess,
            this.TopPlayersLimit,
            cancellationToken);

        this.Standings[newGuess.GameId] = newStandings;

        await this.StandingsDispatcher.PublishStandingsAsync(
            newStandings,
            cancellationToken);
    }
}