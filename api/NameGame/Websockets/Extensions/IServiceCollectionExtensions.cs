using NameGame.Websockets.Dispatchers;

namespace NameGame.Websockets.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDispatchers(
        this IServiceCollection services)
    {
        services.AddSingleton<IGuessDispatcher, GuessDispatcher>();

        return services;
    }
}