// Data Source=rawaaDB.mssql.somee.com;Initial Catalog=rawaaDB;user id=mfhelal12345_SQLLogin_1; pwd=jlgjukukt9;
//Server=.\\SQLExpress;Database=RwaaaDB;Trusted_Connection=True;
//Scaffold - DbContext "Server=.\SQLExpress;Database=LocalRwaaaDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir NewModels--force

using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.ControlPanel;
using Rawaa_Api.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Inject
builder.Services.AddScoped<IProvider<Restaurant>, RestaurantData>();
builder.Services.AddScoped<IProvider<Product>, Rawaa_Api.Services.ProductData>();
builder.Services.AddScoped<IProvider<CategoryRq>, CategoryData>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
