using Microsoft.OpenApi.Models;
using NameGame.Services;
using NameGame.Websockets.Extensions;
using NameGame.Websockets.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for controllers
builder.Services.AddControllers();

builder.Services
    .AddSingleton<IGuessingService, GuessingService>()
    .AddScoped<IWebSocketService, WebsocketService>()
    .AddDispatchers();

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
