using Api;
using Api.Features.Products.Repositories;
using Api.Features.Products.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// add scoped
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<typeof(IGenericRepository<>()), typeof(GenericRepository<>)>;
builder.Services.AddScoped<IProductBrandsRepository, ProductBrandsRepository>();
builder.Services.AddScoped<IProductBrandsService, ProductBrandsService>();
builder.Services.AddScoped<IProductTypesService, ProductTypesService>();
builder.Services.AddScoped<IProductTypesRepository, ProductTypesRepository>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

try
{
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    await ApplicationDataSeeder.SeedAsync(dbContext);
}
catch (Exception ex)
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during the database migration process");
}

app.Run();