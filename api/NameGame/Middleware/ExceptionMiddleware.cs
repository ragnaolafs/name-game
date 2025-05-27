using NameGame.Exceptions;
using System.Text.Json;

namespace NameGame.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode = 500;
        string message = "An unexpected error occurred.";
        List<object> data = [];

        if (exception is GameNotFoundException gameNotFoundException)
        {
            statusCode = 404;
            message = gameNotFoundException.Message;
        }

        if (exception is GameNotActiveException gameNotActiveException)
        {
            statusCode = 400;
            message = gameNotActiveException.Message;
            data.Add(new { status = gameNotActiveException.Status });
        }

        if (exception is GuessAlreadySubmittedException guessAlreadySubmittedException)
        {
            statusCode = 400;
            message = guessAlreadySubmittedException.Message;
            data.Add(new
            {
                gameId = guessAlreadySubmittedException.GameId,
                guess = guessAlreadySubmittedException.Guess
            });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new { error = message, data };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
