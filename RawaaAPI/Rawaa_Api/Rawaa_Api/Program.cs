using Rawaa_Api.Models;
using Rawaa_Api.Services;
using Rawaa_Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Inject
builder.Services.AddScoped<IProvider<Restaurant>, RestaurantData>();
builder.Services.AddScoped<IProvider<Product>, ProductData>();
builder.Services.AddScoped<IProvider<Category>, CategoryData>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
