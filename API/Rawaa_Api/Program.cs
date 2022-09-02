using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Rawaa_Api.Models;
using Rawaa_Api.Services;
using Rawaa_Api.Services.Interfaces;
// Data Source=rawaaDB.mssql.somee.com;Initial Catalog=rawaaDB;user id=mfhelal12345_SQLLogin_1; pwd=jlgjukukt9;
//Data Source=DESKTOP-UND6KAU\\SQLEXPRESS;Initial Catalog=test; Integrated Security=True
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDirectoryBrowser();

// Inject
builder.Services.AddScoped<IProvider<Restaurant>, RestaurantData>();
builder.Services.AddScoped<IProvider<Product>, ProductData>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
       Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/StaticFiles"
});


//http://192.168.1.101:5129/MyImages/image1.jpg;


app.UseAuthorization();

app.MapControllers();

app.Run();
