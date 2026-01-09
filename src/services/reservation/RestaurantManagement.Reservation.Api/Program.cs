using RestaurantManagement.Reservation.Api;
using RestaurantManagement.Reservation.Api.Features.Reservations;
using RestaurantManagement.Reservation.Api.Features.Tables;
using RestaurantManagement.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCommonServiceExt(typeof(TableAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(x => { });

app.AddTableGroupEndpointExt(app.AddVersionSetExt());
app.AddReservationGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();

app.Run();

