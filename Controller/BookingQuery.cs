using GraphQL_APIs.Entities;
using GraphQL_APIs.Service;
using GraphQL_APIs.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_APIs.Controller;

[ExtendObjectType(typeof(QueryType))]
public class BookingQuery
{
    public async Task<IEnumerable<Booking>> GetAllAsync([Service] IBookingService bookingService)
        => await bookingService.GetAllAsync().ToListAsync();
    
    public async Task<IEnumerable<Booking>> GetByIdAsync([Service] IBookingService bookingService, string id)
        => await bookingService.GetByIdAsync(id).ToListAsync();
}