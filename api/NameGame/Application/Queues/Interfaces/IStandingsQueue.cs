using NameGame.Models.Results;

namespace NameGame.Application.Queues.Interfaces;

public interface IStandingsQueue
{
    ValueTask EnqueueUpdateStandingsAsync(
        GuessResult guess,
        CancellationToken cancellationToken);

    IAsyncEnumerable<GuessResult> ReadAllAsync(
        CancellationToken cancellationToken);

}