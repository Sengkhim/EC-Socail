using GraphQL_APIs.Entities;
using GraphQL_APIs.Module;

namespace GraphQL_APIs.Service;

public interface IBookingService
{
    IQueryable<Booking> GetAllAsync();

    IQueryable<Booking> GetByIdAsync(string id);
}

public interface IBookingMutationService
{
    Task<Booking> CreateAsync(BookingInput input, CancellationToken token);
}