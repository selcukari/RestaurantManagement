using RestaurantManagement.Discount.Api;
using RestaurantManagement.Discount.Api.Features.Discounts;
using RestaurantManagement.Discount.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();

builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(x => { });

app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();
}

//app.UseAuthentication();
//app.UseAuthorization();


app.Run();
