using GraphQL_APIs.Database;
using GraphQL_APIs.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_APIs.Schema;

public abstract class Query
{
    public string Text() => "Hello Graph QL";
    public async Task<List<Booking>> GetAllAsync([Service] AppDbContext service, CancellationToken cancellationToken)
    => await service.Set<Booking>().ToListAsync(cancellationToken);
}

// public abstract class QueryType(AppDbContext context) : ObjectType<Query>
// {
//     protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
//     {
//         descriptor
//             .Field(f => f.GetAllAsync(context));
//     }
// }