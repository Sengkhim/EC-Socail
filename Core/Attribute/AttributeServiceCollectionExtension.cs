using System.Reflection;

namespace GraphQL_APIs.Core.Attribute;

public static class AttributeServiceCollectionExtension
{
    public static void AddServiceAttributeHandler(this IServiceCollection serviceCollection, IHostEnvironment hostEnvironment)
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(assembly => assembly.GetName().Name == hostEnvironment.ApplicationName);
        
        var servicesImpls = assembly?.GetTypes().Where(type =>
                type.GetCustomAttribute<InjectServiceAttribute>()?.GetType() == typeof(InjectServiceAttribute))
            .ToArray();
        
        if(servicesImpls is null) return; 

        foreach (var servicesImpl in servicesImpls!)
        {
            var serviceLifetime = servicesImpl.GetCustomAttribute<InjectServiceAttribute>()!.ServiceLifetime;

            foreach (var service in servicesImpl.GetInterfaces())
            {
                Action<IServiceCollection> resolve = serviceLifetime switch
                {
                    ServiceLifetime.Scoped => (collection)
                    => collection.AddScoped(service, servicesImpl),
                    
                    ServiceLifetime.Transient => (collection)
                        => collection.AddScoped(service, servicesImpl),
                    
                    ServiceLifetime.Singleton => (collection)
                        => collection.AddSingleton(service, servicesImpl),
                    
                    #pragma warning disable CA2208
                    _ => throw new ArgumentOutOfRangeException()
                    #pragma warning restore CA2208
                };
                
                resolve.Invoke(serviceCollection);
            }
        }
    }
}