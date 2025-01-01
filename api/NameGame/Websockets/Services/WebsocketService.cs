using System.Net.WebSockets;

namespace NameGame.Websockets.Services;

public class WebsocketService : IWebSocketService
{
    public async Task<WebSocket?> AcceptWebSocketAsync(HttpContext httpContext)
    {
        if (!httpContext.WebSockets.IsWebSocketRequest)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = "This endpoint only accepts websocket requests"
            });

            return null;
        }

        return await httpContext.WebSockets.AcceptWebSocketAsync();
    }
}