using Microsoft.EntityFrameworkCore;
using NameGame.Application.Queues.Interfaces;
using NameGame.Application.Services.Interfaces;
using NameGame.Data.Contexts;
using NameGame.Data.Entities;
using NameGame.Data.Extensions;
using NameGame.Exceptions;
using NameGame.Models.Enums;
using NameGame.Models.Requests;
using NameGame.Models.Results;

namespace NameGame.Application.Services;

public class GameService(
    ILogger<GameService> logger,
    IGuessQueue guessQueue,
    IDbContextFactory<NameGameDbContext> dbContextFactory,
    IConfiguration configuration)
    : IGameService
{
    private ILogger<GameService> Logger { get; } = logger;

    private IGuessQueue GuessQueue { get; } = guessQueue;

    private NameGameDbContext DbContext { get; } = dbContextFactory.CreateDbContext();

    private int TopPlayersLimit { get; }
        = configuration.GetValue<int?>("TopPlayersLimit") ?? 10;

    public async Task<CreateGameResult> CreateGameAsync(
        CreateGameRepuest createGameRepuest,
        CancellationToken cancellationToken)
    {
        var newgame = new GameEntity
        {
            Answer = createGameRepuest.Answer,
            Status = GameStatus.Setup
        };

        await this.DbContext.Games.AddAsync(newgame, cancellationToken);

        await this.DbContext.SaveChangesAsync(cancellationToken);

        this.Logger.LogInformation("New game created. Handle: {handle}", newgame.Handle);

        return new CreateGameResult(newgame.Id, newgame.Status);
    }

    public async Task<StartGameResult> StartGameAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var game = await this.DbContext.Games
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken)
                ?? throw new GameNotFoundException(id);

        game.Status = GameStatus.Active;

        await this.DbContext.SaveChangesAsync(cancellationToken);

        this.Logger.LogInformation("Game started! [{handle}]", game.Handle);

        return new StartGameResult(game.Id, game.Handle, game.Status);
    }

    public async Task<GetGameResult> GetGameAsync(
        string gameId,
        CancellationToken cancellationToken)
    {
        var game = await this.DbContext.Games
            .FirstOrDefaultAsync(g => g.Id == gameId, cancellationToken)
                ?? throw new GameNotFoundException(gameId);

        var standings = await this.DbContext.Guesses.CalculateStandings(
            gameId,
            this.TopPlayersLimit,
            cancellationToken);

        return new GetGameResult(gameId, game.Status, game.Handle, standings);
    }

    public async Task SubmitGuessAsync(
        AddGuessInput input,
        CancellationToken cancellationToken)
    {
        this.Logger.LogInformation("Receiving a new guess. {user} guessed: {guess}", input.User, input.Guess);

        // todo: check if game is active

        // todo: check if guess has already been submitted

        await GuessQueue.EnqueueGuessAsync(input, cancellationToken);
    }
}