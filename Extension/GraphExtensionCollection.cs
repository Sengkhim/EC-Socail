using System.Diagnostics;
using System.Reflection;
using GraphQL_APIs.Database;
using GraphQL_APIs.Types;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_APIs.Extension;

public static class GraphExtensionCollection
{
    private static void AddTypeExtensionLayer(this IRequestExecutorBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        // Iterate all query extensions type
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            var queryTypes = types.Where(type => type.GetCustomAttribute<ExtendObjectTypeAttribute>()?.ExtendsType == typeof(QueryType));
           
            foreach (var queryType in queryTypes)
                builder.AddTypeExtension(queryType);
        }

        // Iterate all mutation extensions type
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            var mutationTypes = types.Where(type => type.GetCustomAttribute<ExtendObjectTypeAttribute>()?.ExtendsType == typeof(MutationType));
           
            foreach (var mutationType in mutationTypes)
                builder.AddTypeExtension(mutationType);
        }
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