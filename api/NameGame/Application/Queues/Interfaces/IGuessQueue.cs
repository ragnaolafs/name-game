using NameGame.Models.Requests;

namespace NameGame.Application.Queues.Interfaces;

public interface IGuessQueue
{
    ValueTask EnqueueGuessAsync(GuessRequest request, CancellationToken cancellationToken);

    IAsyncEnumerable<GuessRequest> ReadAllAsync(CancellationToken cancellationToken);
}