using System.Net.WebSockets;

namespace NameGame.Websockets.Services;

public interface IWebSocketService
{
    Task<WebSocket?> AcceptWebSocketAsync(HttpContext httpContext);
}