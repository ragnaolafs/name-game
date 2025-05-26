namespace NameGame.Exceptions;

// todo map this to 404 globally
public class GameNotFoundException(string id) :
    Exception($"Game not found with id: {id}")
{
}