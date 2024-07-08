using System.Diagnostics;
using System.Reflection;
using GraphQL_APIs.Database;
using GraphQL_APIs.Module;
using GraphQL_APIs.Service;
using GraphQL_APIs.Types;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace GraphQL_APIs.Extension;

public static class GraphExtensionCollection
{
    private static void AddTypeExtensionLayer(this IRequestExecutorBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        // Iterate all query extensions type
        foreach (var assembly in assemblies)
        {
            var queryTypes = assembly.GetTypes().Where(type => 
                type.GetCustomAttribute<ExtendObjectTypeAttribute>()?.ExtendsType == typeof(QueryType));
           
            foreach (var queryType in queryTypes)
                builder.AddTypeExtension(queryType);
        }

        // Iterate all mutation extensions type
        foreach (var assembly in assemblies)
        {
            var mutationTypes = assembly.GetTypes().Where(type => 
                type.GetCustomAttribute<ExtendObjectTypeAttribute>()?.ExtendsType == typeof(MutationType));
            
            foreach (var mutationType in mutationTypes)
                builder.AddTypeExtension(mutationType);
        }
    }

    public static void AddInfrastructureLayer(this IServiceCollection service)
    {
        service.AddScoped<IBookingService, BookingQueryService>();
        service.AddScoped<IBookingMutationService, BookingMutationService>();
        service.AddSingleton<IConnectionMultiplexer>(_ => 
            ConnectionMultiplexer.Connect("localhost:6379"));
    }

    public static void AddCoreLayer(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddCors(options =>
        {
            configuration.GetValue<List<string>>("AllowOrigins");
            options.AddDefaultPolicy(policyBuilder => {
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });    
    }

    public static void AddDatabaseLayer(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("K_Sport_DB"));
            opt.LogTo((logInfo) => Debug.Write(logInfo), LogLevel.Information);
        });
    }
    
    public static void AddGraphQlServerLayer(this IServiceCollection service)
    {
        service
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddTypeExtensionLayer();
    }
}