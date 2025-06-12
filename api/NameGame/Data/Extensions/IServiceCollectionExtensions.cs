using Microsoft.EntityFrameworkCore;
using NameGame.Data.Contexts;

namespace NameGame.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNameGameDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextFactory<NameGameDbContext>(opt =>
        {
            var connectionString = configuration.GetConnectionString("NameGameDatabase");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string for 'NameGameDatabase' is missing.");
            }

            opt.UseNpgsql(connectionString);
        });

        return services;
    }
}