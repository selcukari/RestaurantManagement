using RestaurantManagement.Kitchen.Api;
using RestaurantManagement.Kitchen.Api.Features.Kitchens;
using RestaurantManagement.Kitchen.Api.Options;
using RestaurantManagement.Shared.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(KitchenAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

app.UseExceptionHandler(x => { });
app.AddKitchenGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.Run();
