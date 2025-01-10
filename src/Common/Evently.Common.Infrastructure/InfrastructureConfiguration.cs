using Evently.Common.Application.Caching;
using Npgsql;
using Evently.Common.Application.Data;
using Evently.Common.Application.Clock;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache")!;
        string databaseConnectionString = configuration.GetConnectionString("Database")!;
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.TryAddSingleton(connectionMultiplexer);

        services.AddStackExchangeRedisCache(options =>
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        
        services.TryAddSingleton<ICacheService, CacheService>();

        return services;
    }
}
