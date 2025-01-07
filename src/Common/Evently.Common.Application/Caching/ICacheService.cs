using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Application.Caching;

public interface ICacheService
{
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory, 
        DistributedCacheEntryOptions? options = null, 
        CancellationToken cancellationToken = default
    );
}
