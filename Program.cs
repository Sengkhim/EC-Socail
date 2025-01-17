using GraphQL_APIs.Exception;
using GraphQL_APIs.Extension;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseLayer(builder.Configuration);
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddGraphQlServerLayer();
builder.Services.AddInfrastructureLayer(builder.Environment);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UsePlayground("/graphql");
app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();

