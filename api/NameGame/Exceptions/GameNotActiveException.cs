using NameGame.Data.Entities;
using NameGame.Models.Enums;

namespace NameGame.Exceptions;

public class GameNotActiveException(GameEntity game) :
    Exception($"Game is not active. Status: {game.Status}")
{
    public GameStatus Status { get; } = game.Status;
}