using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.ControlPanel;
using Rawaa_Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Inject
    builder.Services.AddScoped<IProvider<Restaurant>, RestaurantData>();
    builder.Services.AddScoped<IProvider<CategoryRq>, CategoryData>();
    builder.Services.AddScoped<IProductData, ProductData>();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddCors();
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    app.UseCors(w => w.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
