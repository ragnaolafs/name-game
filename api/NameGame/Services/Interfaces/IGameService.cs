using NameGame.Models.Results;

namespace NameGame.Services;

public interface IGameService
{
    public Task<CreateGameResult> CreateGameAsync(CancellationToken cancellationToken);

    Task StartGameAsync(CancellationToken cancellationToken);
}