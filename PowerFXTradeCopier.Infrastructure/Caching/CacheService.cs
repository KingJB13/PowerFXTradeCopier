using Microsoft.Extensions.Caching.Distributed;
using PowerFXTradeCopier.Application.Abstractions.Caching;
using System.Text.Json;

namespace PowerFXTradeCopier.Infrastructure.Caching;

public sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _cache.GetStringAsync(key);

        if (value is null)
            return default;

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null)
    {
        var serialized = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(
            key,
            serialized,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    expiration ?? TimeSpan.FromMinutes(30)
            });
    }
    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}
