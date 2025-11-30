using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Order.Api.Endpoints.Orders;
using RestaurantManagement.Order.Application;
using RestaurantManagement.Bus;
using RestaurantManagement.Order.Application.Contracts.Repositories;
using RestaurantManagement.Order.Application.Contracts.UnitOfWork;
using RestaurantManagement.Order.Persistence;
using RestaurantManagement.Order.Persistence.Repositories;
using RestaurantManagement.Order.Persistence.UnitOfWork;
using RestaurantManagement.Shared.Extensions;
using RestaurantManagement.Order.Application.Contracts.Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersioningExt();
// builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);

var app = builder.Build();
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


//app.UseAuthentication();
//app.UseAuthorization();

app.Run();