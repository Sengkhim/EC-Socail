using GraphQL_APIs.Database;
using GraphQL_APIs.Entities;
using GraphQL_APIs.Service;

namespace GraphQL_APIs.Module;
public class BookingQueryService(AppDbContext context) : IBookingService
{
    public IQueryable<Booking> GetAllAsync()
        => context.Set<Booking>().AsQueryable();
    
    public IQueryable<Booking> GetByIdAsync(string id)
        => context.Set<Booking>().Where(e => e.Id == id);
}