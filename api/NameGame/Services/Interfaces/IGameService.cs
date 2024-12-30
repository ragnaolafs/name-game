using NameGame.Models;

namespace NameGame.Services;

public interface IGameService
{
    public Task<Game> CreateGameAsync(CancellationToken cancellationToken);

    Task StartGameAsync(CancellationToken cancellationToken);
}