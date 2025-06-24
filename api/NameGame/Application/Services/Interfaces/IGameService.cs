using NameGame.Models.Requests;
using NameGame.Models.Results;

namespace NameGame.Application.Services.Interfaces;

public interface IGameService
{
    Task<CreateGameResult> CreateGameAsync(
        CreateGameRepuest createGameRepuest,
        CancellationToken cancellationToken);

    Task<GetGameResult> GetGameAsync(
        string id,
        CancellationToken cancellationToken);

    Task<GetGameResult> GetGameByHandle(
        string handle,
        CancellationToken cancellationToken);

    Task<StartGameResult> StartGameAsync(
        string id,
        CancellationToken cancellationToken);

    Task SubmitGuessAsync(
        AddGuessInput input,
        CancellationToken cancellationToken);

    Task<ICollection<GuessResult>> GetGuessesAsync(
        string gameId,
        GetGuessesFilter filter,
        CancellationToken cancellationToken);

    Task<GameStatusResult> GetGameStatusAsync(
        string id,
        CancellationToken cancellationToken);
}