using GraphQL_APIs.Entities;
using GraphQL_APIs.Implementation;
using GraphQL_APIs.Service;
using GraphQL_APIs.Types;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace GraphQL_APIs.Controller;

[ExtendObjectType(typeof(QueryType))]
public class BookingQuery
{
    public async Task<IEnumerable<Booking>> GetAllAsync(
            [Service] IBookingService bookingService
        ) => await bookingService.GetAllAsync().ToListAsync();

    public async Task<IEnumerable<Booking>> GetAllCachesAsync(
            [Service] IBookingService bookingService,
            [Service] IConnectionMultiplexer redis
        ) => await bookingService.GetAllCacheAsync(redis);

    public async Task<IEnumerable<Booking>> GetBookingsRedis(
        [Service] ICacheService cacheService,
        [Service] IConnectionMultiplexer redis
    ) => await cacheService.TryGet(redis);
}