using NameGame.Websockets.Dispatchers;
using NameGame.Websockets.Services;

namespace NameGame.Websockets.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWebSocketServices(
        this IServiceCollection services)
    {
        services
        .AddSingleton<IGuessDispatcher, GuessDispatcher>()
        .AddScoped<IWebSocketService, WebsocketService>();

        return services;
    }
}