using RestaurantManagement.Menu.Api.Features.Products;
using RestaurantManagement.Reporting.Api;
using RestaurantManagement.Reporting.Api.Options;
using RestaurantManagement.Reporting.Api.Repositories;
using RestaurantManagement.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(ReportingAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(x => { });

app.AddReportingGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.


app.UseAuthentication();
app.UseAuthorization();

app.Run();