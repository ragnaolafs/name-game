using NameGame.Models.Messages;

namespace NameGame.Application.Queues.Interfaces;

public interface IStatusQueue
{
    ValueTask EnqueueUpdateStatusAsync(
        GameEvent gameEvent,
        CancellationToken cancellationToken);

    IAsyncEnumerable<GameEvent> ReadAllAsync(
        CancellationToken cancellationToken);
}