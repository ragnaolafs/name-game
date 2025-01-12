using Microsoft.EntityFrameworkCore;
using NameGame.Application.Services.Interfaces;
using NameGame.Data.Contexts;
using NameGame.Data.Entities;
using NameGame.Exceptions;
using NameGame.Models.Enums;
using NameGame.Models.Requests;
using NameGame.Models.Results;

namespace NameGame.Application.Services;

public class GameService(
    NameGameDbContext dbContext)
    : IGameService
{
    private NameGameDbContext DbContext { get; } = dbContext;

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

        return new StartGameResult(game.Id, game.Handle, game.Status);
    }
}