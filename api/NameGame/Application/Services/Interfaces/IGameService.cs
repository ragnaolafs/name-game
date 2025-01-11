using NameGame.Models.Requests;
using NameGame.Models.Results;

namespace NameGame.Application.Services.Interfaces;

public interface IGameService
{
    public Task<CreateGameResult> CreateGameAsync(
        CreateGameRepuest createGameRepuest,
        CancellationToken cancellationToken);

    Task StartGameAsync(CancellationToken cancellationToken);
}