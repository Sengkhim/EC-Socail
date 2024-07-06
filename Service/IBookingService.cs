using GraphQL_APIs.Entities;
using GraphQL_APIs.Module;
using StackExchange.Redis;

namespace GraphQL_APIs.Service;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetAllCacheAsync(IConnectionMultiplexer redis);
    IQueryable<Booking> GetAllAsync();
}

public interface IBookingMutationService
{
    Task<Booking> CreateAsync(BookingInput input, CancellationToken token);
}