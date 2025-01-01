
using System.Threading.Channels;
using NameGame.Application.Queues.Interfaces;
using NameGame.Models.Requests;

namespace NameGame.Application.Queues;

public class GuessQueue() : IGuessQueue
{
    private readonly Channel<GuessRequest> _channel =
        Channel.CreateUnbounded<GuessRequest>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });

    public ValueTask EnqueueGuessAsync(
        GuessRequest request,
        CancellationToken cancellationToken)
    {
        return _channel.Writer.WriteAsync(request, cancellationToken);
    }

    public IAsyncEnumerable<GuessRequest> ReadAllAsync(
        CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}