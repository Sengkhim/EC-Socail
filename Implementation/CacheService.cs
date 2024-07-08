using System.Text.Json;
using GraphQL_APIs.Core.Attribute;
using GraphQL_APIs.Entities;
using StackExchange.Redis;

namespace GraphQL_APIs.Implementation;

public interface ICacheService
{
    Task<IEnumerable<Booking>>TryGet(IConnectionMultiplexer redis);
}

[InjectService(ServiceLifetime.Transient)]
public class CacheService : ICacheService
{
    public async Task<IEnumerable<Booking>> TryGet(IConnectionMultiplexer redis)
    {
        var cachedBook = redis.GetDatabase().StringGet($"book");
        
        var response = JsonSerializer.Deserialize<Booking[]>(cachedBook!);
        return await Task.FromResult(response!.ToList());
    }
}