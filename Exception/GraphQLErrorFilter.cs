
namespace GraphQL_APIs.Exception;

public class GraphQlErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if(error.Exception != null)
            return error.WithMessage(error.Exception.Message).RemoveExtensions().RemoveLocations();
        return error.WithMessage(error.Message).RemoveExtensions().RemoveLocations();
    }
}