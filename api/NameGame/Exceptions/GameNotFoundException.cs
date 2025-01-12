namespace NameGame.Exceptions;

public class GameNotFoundException(string id) :
    Exception($"Game not found with id: {id}")
{
}