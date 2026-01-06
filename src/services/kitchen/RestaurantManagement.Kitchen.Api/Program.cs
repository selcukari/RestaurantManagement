using RestaurantManagement.Kitchen.Api;
using RestaurantManagement.Kitchen.Api.Options;
using RestaurantManagement.Shared.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptionsExt();
builder.Services.AddCommonServiceExt(typeof(KitchenAssembly));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
