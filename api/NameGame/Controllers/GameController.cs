namespace NameGame.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NameGame.Models.Requests;
using NameGame.Services;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    [HttpPost("{id}/guess")]
    public async Task<IActionResult> SubmitGuess(
        [FromRoute] string id,
        [FromBody] GuessRequest request,
        [FromServices] IGuessingService guessingService,
        CancellationToken cancellationToken)
    {
        await guessingService.AddGuessAsync(request, cancellationToken);

        return Ok(new { });
    }
}
