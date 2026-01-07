using RestaurantManagement.Kitchen.Api;
using RestaurantManagement.Kitchen.Api.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(KitchenAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
