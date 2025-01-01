using Microsoft.EntityFrameworkCore;
using NameGame.Data.Contexts;

namespace NameGame.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNameGameDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // services.AddPooledDbContextFactory<NameGameDbContext>(o =>
        //     configuration.GetConnectionString("NameGameDatabase"));

        services.AddDbContextFactory<NameGameDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("NameGameDatabase")));

        return services;
    }
}