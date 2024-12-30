using System.Net.WebSockets;
using NameGame.Models.Requests;

namespace NameGame.Websockets.Dispatchers;

public interface IGuessDispatcher
{
    Task PublishGuessAsync(GuessRequest guess, CancellationToken cancellationToken);

    Task SubscribeToGuessesAsync(WebSocket webSocket, CancellationToken cancellationToken);
}