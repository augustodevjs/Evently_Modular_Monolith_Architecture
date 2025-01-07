using System.Text.Json;
using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;

public class CacheService(IDistributedCache cache) : ICacheService
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private static readonly DistributedCacheEntryOptions Default = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    public async Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        DistributedCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        string cachedValue = await cache.GetStringAsync(key, cancellationToken);

        if (!string.IsNullOrWhiteSpace(cachedValue))
        {
            T? value = Deserialize<T>(cachedValue);
            if (value is not null)
            {
                return value;
            }
        }

        if (!await Semaphore.WaitAsync(2000, cancellationToken))
        {
            return default;
        }

        try
        {
            cachedValue = await cache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrWhiteSpace(cachedValue))
            {
                T? value = Deserialize<T>(cachedValue);
                if (value is not null)
                {
                    return value;
                }
            }

            T newValue = await factory();

            if (newValue is null)
            {
                return default;
            }

            await cache.SetStringAsync(key, Serialize(newValue), options ?? Default, cancellationToken);

            return newValue;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value);
    }

    private static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}
