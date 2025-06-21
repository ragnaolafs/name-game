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
        // todo make this return game handle
        var game = await gameService.CreateGameAsync(
            createGameRepuest,
            cancellationToken);

        return this.Ok(game);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameAsync(
        [FromRoute] string id,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        var game = await gameService.GetGameAsync(id, cancellationToken);

        return this.Ok(game);
    }

    [HttpGet("{id}/guesses")]
    public async Task<IActionResult> GetGuessesAsync(
        [FromRoute] string id,
        [FromQuery] GetGuessesFilter? filter,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        filter ??= new GetGuessesFilter();

        var guesses = await gameService.GetGuessesAsync(
            id,
            filter,
            cancellationToken);

        return this.Ok(guesses);
    }

    [HttpGet("join/{handle}")]
    public async Task<IActionResult> JoinGameAsync(
        [FromRoute] string handle,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        var result = await gameService.GetGameByHandle(handle, cancellationToken);

        return this.Ok(result);
    }

    [HttpPost("{id}/guess")]
    public async Task<IActionResult> SubmitGuessAsync(
        [FromRoute] string id,
        [FromBody] AddGuessRequest request,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        var input = new AddGuessInput(id, request.User, request.Guess);

        await gameService.SubmitGuessAsync(input, cancellationToken);

        return this.Ok(new { });
    }

    [HttpPatch("{id}/start")]
    public async Task<IActionResult> StartGameAsync(
        [FromRoute] string id,
        [FromServices] IGameService gameService,
        CancellationToken cancellationToken)
    {
        var result = await gameService.StartGameAsync(id, cancellationToken);

        return this.Ok(result);
    }
}