using System.Text.Json;
using GraphQL_APIs.Database;
using GraphQL_APIs.Entities;
using GraphQL_APIs.Service;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace GraphQL_APIs.Module;
public class BookingQueryService(AppDbContext context) : IBookingService
{
    public IQueryable<Booking> GetAllAsync() => context.Set<Booking>().AsQueryable();

    public async Task<IEnumerable<Booking>> GetAllCacheAsync(IConnectionMultiplexer redis)
    {
        var cachedBook = redis.GetDatabase().StringGet($"book");
        
        if (cachedBook.HasValue)
        {
            var response = JsonSerializer.Deserialize<Booking[]>(cachedBook!);
            return await Task.FromResult(response!.ToList());
        }

        var book = await context.Set<Booking>().AsQueryable().ToListAsync();
        redis.GetDatabase().StringSet($"book", JsonSerializer.Serialize(book));
        return await Task.FromResult(book);
    }
}