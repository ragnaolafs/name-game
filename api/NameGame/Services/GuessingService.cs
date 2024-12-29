using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using NameGame.Models.Requests;

namespace NameGame.Services;

public class GuessingService(
    ILogger<GuessingService> logger)
    : IGuessingService
{
    private ILogger<GuessingService> Logger { get; } = logger;

    private ConcurrentBag<WebSocket> Clients { get; } = [];

    public async Task AddGuessAsync(GuessRequest request, CancellationToken cancellationToken)
    {
        this.Logger.LogInformation("Receiving a new guess. {user} guessed: {guess}", request.User, request.Guess);

        var serialized = JsonSerializer.Serialize(request);
        var buffer = Encoding.UTF8.GetBytes(serialized);

        var subscriptionTasks = new List<Task>();

        foreach (var client in this.Clients)
        {
            if (client.State is WebSocketState.Open)
            {
                subscriptionTasks.Add(client.SendAsync(
                    new ArraySegment<byte>(buffer),
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken));
            }
        }

        await Task.WhenAll(subscriptionTasks);
    }

    public async Task ListenToGuessesAsync(
        WebSocket webSocket,
        CancellationToken cancellationToken)
    {
        this.Logger.LogInformation("Receiving new websocket connection.");

        this.Clients.Add(webSocket);

        var buffer = new byte[1024 * 4];

        while (webSocket.State is WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                cancellationToken);

            if (result.MessageType is WebSocketMessageType.Close)
            {
                this.Logger.LogInformation("Closing websocket connection...");

                this.Clients.TryTake(out _);
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closed by server",
                    cancellationToken);
            }
        }
    }
}