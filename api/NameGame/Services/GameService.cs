using NameGame.Models.Results;

namespace NameGame.Services;

public class GameService : IGameService
{
    public Task<CreateGameResult> CreateGameAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StartGameAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
