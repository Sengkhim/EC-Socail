namespace GraphQL_APIs.Core.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class InjectServiceAttribute(ServiceLifetime serviceLifetime) : System.Attribute
{
    public ServiceLifetime ServiceLifetime { get; } = serviceLifetime;
}