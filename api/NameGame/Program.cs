using Microsoft.OpenApi.Models;
using NameGame.Application.Extensions;
using NameGame.Data.Extensions;
using NameGame.Websockets.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services for controllers
services.AddControllers();

services
    .AddGameServices()
    .AddWebSocketServices()
    .AddNameGameDatabase(configuration);

services.AddLogging();

// Add Swagger services
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
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