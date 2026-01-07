using RestaurantManagement.Kitchen.Api;
using RestaurantManagement.Kitchen.Api.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(KitchenAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);


var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.Run();
