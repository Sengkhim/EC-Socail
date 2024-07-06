
using GraphQL_APIs.Extension;
using GraphQL_APIs.Module;
using GraphQL_APIs.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseLayer(builder.Configuration);
builder.Services.AddCoreLayer(builder.Configuration);
builder.Services.AddGraphQlServerLayer();
builder.Services.AddScoped<IBookingService, BookingQueryService>();
builder.Services.AddScoped<IBookingMutationService, BookingMutationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();

