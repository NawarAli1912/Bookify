using Bookify.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Buffers;
using System.Text.Json;

namespace Bookify.Infrastructure.Caching;
internal sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? duration = null, CancellationToken cancellationToken = default)
    {
        byte[] bytes = Serialize(value);

        return _distributedCache.SetAsync(key, bytes, CacheOptions.Create(duration), cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var byteArray = await _distributedCache.GetAsync(key, cancellationToken);

        return byteArray is null ? default : Deserialize<T>(byteArray);
    }

    private static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();

        using var writer = new Utf8JsonWriter(buffer);

        JsonSerializer.Serialize(writer, value);

        return buffer.WrittenSpan.ToArray();
    }


    private static T Deserialize<T>(byte[] byteArray)
    {
        return JsonSerializer.Deserialize<T>(byteArray) ??
            throw new ApplicationException("byteArray shouldn't be null.");
    }

}

public static class CacheOptions
{
    public static DistributedCacheEntryOptions Default => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? duration) =>
        duration is null ? Default : new()
        {
            AbsoluteExpirationRelativeToNow = duration
        };
}