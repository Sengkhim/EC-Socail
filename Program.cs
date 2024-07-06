using System.Diagnostics;
using GraphQL_APIs.Schema;
using GraphQL_APIs.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var connection = builder.Configuration.GetConnectionString("K_Sport_DB");
    opt.UseNpgsql(connection);
    opt.LogTo((logInfo) => Debug.Write(logInfo), LogLevel.Information);
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();
