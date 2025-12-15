using RestaurantManagement.Menu.Api;
using RestaurantManagement.Menu.Api.Features.Menus;
using RestaurantManagement.Menu.Api.Features.Products;
using RestaurantManagement.Menu.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(MenuAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();
// builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.AddMenuGroupEndpointExt(app.AddVersionSetExt());
app.AddProductGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

//app.UseAuthentication();
//app.UseAuthorization();

app.Run();
