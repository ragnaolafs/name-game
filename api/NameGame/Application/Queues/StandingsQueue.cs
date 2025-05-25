using System.Threading.Channels;
using NameGame.Application.Queues.Interfaces;
using NameGame.Models.Results;

namespace NameGame.Application.Queues;

public class StandingsQueue : IStandingsQueue
{
    private readonly Channel<GuessResult> _channel =
        Channel.CreateUnbounded<GuessResult>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });

    public ValueTask EnqueueUpdateStandingsAsync(
        GuessResult guess,
        CancellationToken cancellationToken)
    {
        return _channel.Writer.WriteAsync(guess, cancellationToken);
    }

    public IAsyncEnumerable<GuessResult> ReadAllAsync(
        CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}