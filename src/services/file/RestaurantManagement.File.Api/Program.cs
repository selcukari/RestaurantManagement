using Microsoft.Extensions.FileProviders;
using RestaurantManagement.Discount.Api;
using RestaurantManagement.File.Api;
using RestaurantManagement.File.Api.Features.File;
using RestaurantManagement.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthentication();
app.Run();
