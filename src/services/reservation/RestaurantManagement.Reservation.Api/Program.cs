using RestaurantManagement.Reservation.Api;
using RestaurantManagement.Reservation.Api.BackgroundServices;
using RestaurantManagement.Reservation.Api.Features.Reservations;
using RestaurantManagement.Reservation.Api.Features.Tables;
using RestaurantManagement.Reservation.Api.Options;
using RestaurantManagement.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptionsExt();
builder.Services.AddCommonServiceExt(typeof(TableAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

// back service ile 1 gun bir active gunu gecmiþ reservation IsAvailable(false) ve table status(Empty) do
builder.Services.AddHostedService<StatusReservationBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(x => { });

app.AddTableGroupEndpointExt(app.AddVersionSetExt());
app.AddReservationGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();

app.Run();

