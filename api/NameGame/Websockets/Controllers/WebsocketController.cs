using Microsoft.AspNetCore.Mvc;
using NameGame.Websockets.Dispatchers;
using NameGame.Websockets.Services;

namespace NameGame.Websockets.Controllers;

[ApiController]
[Route("api/ws")]
public class WebsocketController(
    IWebSocketService webSocketService)
    : ControllerBase
{
    private IWebSocketService WebSocketService { get; } = webSocketService;

    [HttpGet("game/{id}/guess-stream")]
    public async Task SubscribeToGuessesAsync(
        string id,
        [FromServices] IGuessDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var webSocket = await this.WebSocketService.AcceptWebSocketAsync(this.HttpContext);

        if (webSocket is null)
        {
            return;
        }

        await dispatcher.SubscribeToGuessesAsync(webSocket, cancellationToken);
    }

    [HttpGet("game/{id}/standings")]
    public async Task SubscribeToStandingsAsync(string id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("game/{id}/status")]
    public async Task SubscribeToGameStatusAsync(string id)
    {
        throw new NotImplementedException();
    }
}