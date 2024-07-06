using GraphQL_APIs.Entities;
using GraphQL_APIs.Module;
using GraphQL_APIs.Service;
using GraphQL_APIs.Types;

namespace GraphQL_APIs.Controller;

[ExtendObjectType(typeof(MutationType))]
public class BookingMutation
{
    public async Task<Booking> CreateAsync(
        [Service] IBookingMutationService service,
        BookingInput input,
        CancellationToken cancellationToken)
    {
        return await service.CreateAsync(input, cancellationToken);
    }
}