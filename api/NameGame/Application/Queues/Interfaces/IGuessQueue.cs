using NameGame.Models.Requests;

namespace NameGame.Application.Queues.Interfaces;

public interface IGuessQueue
{
    ValueTask EnqueueGuessAsync(
        AddGuessInput input,
        CancellationToken cancellationToken);

    IAsyncEnumerable<AddGuessInput> ReadAllAsync(CancellationToken cancellationToken);
}