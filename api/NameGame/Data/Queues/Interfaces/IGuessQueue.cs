using NameGame.Models.Requests;

namespace NameGame.Data.Queues;

public interface IGuessQueue
{
    ValueTask EnqueueGuessAsync(GuessRequest request, CancellationToken cancellationToken);

    IAsyncEnumerable<GuessRequest> ReadAllAsync(CancellationToken cancellationToken);
}