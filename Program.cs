
using GraphQL_APIs.Extension;
using GraphQL_APIs.Module;
using GraphQL_APIs.Service;
using HotChocolate.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseLayer(builder.Configuration);
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddGraphQlServerLayer();
builder.Services.AddScoped<IBookingService, BookingQueryService>();
builder.Services.AddScoped<IBookingMutationService, BookingMutationService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(_ => 
    ConnectionMultiplexer.Connect("localhost:6379"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UsePlayground("/graphql");
app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();

