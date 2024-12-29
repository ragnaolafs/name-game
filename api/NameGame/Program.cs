using Microsoft.OpenApi.Models;
using NameGame.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for controllers
builder.Services.AddControllers();

builder.Services.AddSingleton<IGuessingService, GuessingService>();

builder.Services.AddLogging();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Name Game API",
        Version = "v1 "
    }));

var app = builder.Build();


// WebSocket options
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120)
};

app.UseWebSockets(webSocketOptions);

// WebSocket endpoint for echoing submitted guesses
app.Map("/ws/game/guesses", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

    var guessService = context.RequestServices.GetRequiredService<IGuessingService>();

    await guessService.ListenToGuessesAsync(webSocket, CancellationToken.None);
});

// Map controller routes
app.MapControllers();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Set Swagger UI to the root URL
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Game API v1");
        options.RoutePrefix = string.Empty; // This makes Swagger UI available at the root
    });
}

app.Run();
