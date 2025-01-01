using NameGame.Models.Results;

namespace NameGame.Application.Services.Interfaces;

public interface IGameService
{
    public Task<CreateGameResult> CreateGameAsync(CancellationToken cancellationToken);

    Task StartGameAsync(CancellationToken cancellationToken);
}