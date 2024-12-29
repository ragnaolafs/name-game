using System.Net.WebSockets;
using NameGame.Models.Requests;

namespace NameGame.Services;

public interface IGuessingService
{
    Task AddGuessAsync(GuessRequest guess, CancellationToken cancellationToken);

    Task ListenToGuessesAsync(WebSocket webSocket, CancellationToken cancellationToken);
}