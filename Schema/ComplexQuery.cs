using GraphQL_APIs.Types;

namespace GraphQL_APIs.Schema;

[ExtendObjectType(typeof(QueryType))]
public class ComplexQuery
{
    public string ComplexQueryFilter() => "ComplexQuery";
}