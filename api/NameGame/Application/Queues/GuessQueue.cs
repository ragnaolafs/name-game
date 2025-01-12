
using System.Threading.Channels;
using NameGame.Application.Queues.Interfaces;
using NameGame.Models.Requests;

namespace NameGame.Application.Queues;

public class GuessQueue() : IGuessQueue
{
    private readonly Channel<AddGuessInput> _channel =
        Channel.CreateUnbounded<AddGuessInput>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });

    public ValueTask EnqueueGuessAsync(
        AddGuessInput input,
        CancellationToken cancellationToken)
    {
        return _channel.Writer.WriteAsync(input, cancellationToken);
    }

    public IAsyncEnumerable<AddGuessInput> ReadAllAsync(
        CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}