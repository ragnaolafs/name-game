using Microsoft.EntityFrameworkCore;
using NameGame.Models;
using NameGame.Models.Results;

namespace NameGame.Data.Extensions;

public static class GuessEntityExtensions
{
    public static async Task<StandingsResult> CalculateStandings(
        this DbSet<GuessEntity> guesses,
        string gameId,
        int limit,
        CancellationToken cancellationToken)
    {
        var topGuesses = await guesses
            .Where(g => g.GameId == gameId)
            .OrderByDescending(g => g.Score)
            .Take(limit)
            .Select(g => new GuessResult(
                g.Id,
                g.GameId,
                g.User,
                g.Guess,
                g.Score,
                g.Score * 100))
            .ToListAsync(cancellationToken: cancellationToken);

        return new StandingsResult(gameId, topGuesses, DateTime.UtcNow);
    }
}