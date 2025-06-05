using System.Net.WebSockets;
using NameGame.Models.Results;

namespace NameGame.Websockets.Dispatchers.Interfaces;

public interface IStatusDispatcher
{
    Task PublishGameStatusAsync(
        GameStatusResult status,
        CancellationToken cancellationToken);

    Task SubscribeToGameStatusAsync(
        string gameId,
        WebSocket webSocket,
        CancellationToken cancellationToken);
}
