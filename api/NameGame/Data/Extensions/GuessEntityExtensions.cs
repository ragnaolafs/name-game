using Microsoft.EntityFrameworkCore;
using NameGame.Models;
using NameGame.Models.Results;

namespace NameGame.Data.Extensions;

public static class GuessEntityExtensions
{
    public static async Task<StandingsResult> CalculateStandings(
        this DbSet<GuessEntity> guesses,
        GuessResult incomingGuess,
        int limit,
        CancellationToken cancellationToken)
    {
        var topGuesses = await guesses
            .Where(g => g.GameId == incomingGuess.GameId)
            .OrderByDescending(g => g.Score)
            .Take(limit)
            .Select(g => new GuessResult(
                g.Id,
                g.GameId,
                g.User,
                g.Guess,
                g.Score))
            .ToListAsync(cancellationToken: cancellationToken);

        return new StandingsResult(incomingGuess.GameId, topGuesses);
    }
}