using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Evently.API.Extensions;

internal static class HealthChecksExtensions
{
    internal static IServiceCollection ConfigureApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache")!;
        string databaseConnectionString = configuration.GetConnectionString("Database")!;
        
        services.AddHealthChecks()
            .AddNpgSql(databaseConnectionString)
            .AddRedis(redisConnectionString);

        return services;
    }
    
    internal static void UseApplicationHealthCheck(this WebApplication app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}
