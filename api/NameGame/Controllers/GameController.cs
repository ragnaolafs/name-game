namespace NameGame.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NameGame.Application.Services.Interfaces;
using NameGame.Models.Requests;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> CreateGameAsync(
        [FromBody] CreateGameRepuest createGameRepuest,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        var game = await gameService.CreateGameAsync(
            createGameRepuest,
            cancellationToken);

        return this.Ok(game);
    }

    [HttpPost("{id}/guess")]
    public async Task<IActionResult> SubmitGuessAsync(
        [FromRoute] string id,
        [FromBody] GuessRequest request,
        [FromServices] IGuessingService guessingService,
        CancellationToken cancellationToken)
    {
        await guessingService.SubmitGuessAsync(request, cancellationToken);

        return this.Ok(new { });
    }

    [HttpPatch("{id}/start")]
    public async Task<IActionResult> StartGameAsync(
        [FromRoute] string id,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        await gameService.StartGameAsync(cancellationToken);

        return this.Ok(new { });
    }
}