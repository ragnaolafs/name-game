using NameGame.Application.Services.Interfaces;
using NameGame.Data.Contexts;
using NameGame.Data.Entities;
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

    public Task StartGameAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}