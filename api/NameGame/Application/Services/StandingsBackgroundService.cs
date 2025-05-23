using Microsoft.EntityFrameworkCore;
using NameGame.Application.Queues.Interfaces;
using NameGame.Data.Contexts;
using NameGame.Data.Extensions;
using NameGame.Models.Requests;
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
        AddGuessInput newGuess,
        CancellationToken cancellationToken)
    {
        this.Logger.LogInformation(
            "calculating standings. top player limit is {limit}",
            this.TopPlayersLimit);

        var newStandings = await this.DbContext.Guesses.CalculateStandings(
            newGuess,
            this.TopPlayersLimit,
            cancellationToken);

        // todo does equals this work?
        if (this.Standings.TryGetValue(newGuess.GameId, out var currentStandings)
            && currentStandings.Equals(newStandings))
        {
            this.Logger.LogInformation("Standings are the same. No need to update.");
            return;
        }

        this.Standings[newGuess.GameId] = newStandings;

        await this.StandingsDispatcher.PublishStandingsAsync(
            newStandings,
            cancellationToken);
    }
}