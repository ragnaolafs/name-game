using Microsoft.EntityFrameworkCore;
using NameGame.Data.Entities;
using NameGame.Exceptions;

namespace NameGame.Data.Extensions;

public static class GameEntityExtensions
{
    public static async Task<GameEntity> GetGameByIdAsync(
        this DbSet<GameEntity> games,
        string id,
        CancellationToken cancellationToken)
    {
        return await games.FirstOrDefaultAsync(
            g => g.Id == id,
            cancellationToken)
            ?? throw new GameNotFoundException(id);
    }

    public static async Task<GameEntity> GetGameByHandleAsync(
        this DbSet<GameEntity> games,
        string handle,
        CancellationToken cancellationToken)
    {
        return await games.FirstOrDefaultAsync(
            g => g.Handle == handle,
            cancellationToken)
            ?? throw new GameNotFoundException(handle);
    }
}