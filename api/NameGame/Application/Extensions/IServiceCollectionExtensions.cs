using NameGame.Application.Queues;
using NameGame.Application.Queues.Interfaces;
using NameGame.Application.Services;
using NameGame.Application.Services.Interfaces;
using NameGame.Websockets.Dispatchers;
using NameGame.Websockets.Dispatchers.Interfaces;
using NameGame.Websockets.Services;

namespace NameGame.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddGameServices(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IGuessQueue, GuessQueue>()
            .AddSingleton<IStandingsQueue, StandingsQueue>()
            .AddSingleton<IStatusQueue, StatusQueue>()
            .AddHostedService<GameBackgroundService>()
            .AddHostedService<StandingsBackgroundService>()
            .AddHostedService<StatusBackgroundService>()
            .AddScoped<IGameService, GameService>();

        return services;
    }

    public static IServiceCollection AddWebSocketServices(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IGuessDispatcher, GuessDispatcher>()
            .AddSingleton<IStandingsDispatcher, StandingsDispatcher>()
            .AddSingleton<IStatusDispatcher, StatusDispatcher>()
            .AddScoped<IWebSocketService, WebsocketService>();

        return services;
    }
}