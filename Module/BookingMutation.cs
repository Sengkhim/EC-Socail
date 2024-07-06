using GraphQL_APIs.Database;
using GraphQL_APIs.Entities;
using GraphQL_APIs.Service;

namespace GraphQL_APIs.Module;

public class BookingMutationService(AppDbContext context): IBookingMutationService
{
    private readonly Random _random = new();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    public async Task<Booking> CreateAsync(BookingInput input, CancellationToken token)
    {
        var code = new string(Enumerable.Repeat(Chars, 5)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
        
        var booking = new Booking()
        {
            Id = Guid.NewGuid().ToString(),
            Text = input.Text,
            Description = input.Description,
            Code = code
        };
        
        await context.Set<Booking>().AddAsync(booking, token);
        await context.SaveChangesAsync(token);

        return booking;
    }
}