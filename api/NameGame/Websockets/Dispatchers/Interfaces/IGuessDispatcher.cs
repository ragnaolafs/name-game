using System.Net.WebSockets;
using NameGame.Models.Results;

namespace NameGame.Websockets.Dispatchers;

public interface IGuessDispatcher
{
    Task PublishGuessAsync(
        GuessResult guess,
        CancellationToken cancellationToken);

    Task SubscribeToGuessesAsync(
        string id,
        WebSocket webSocket,
        CancellationToken cancellationToken);
}