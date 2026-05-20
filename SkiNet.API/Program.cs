// ------------------------------------------------------------------------------------------------
// Application Entry Point
// ------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SkiNet.Core.Interfaces;
using SkiNet.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------------------------------------
// Service Registration
// ------------------------------------------------------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddDbContext<SkiNetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkiNetDatabaseConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ------------------------------------------------------------------------------------------------
// Middleware
// ------------------------------------------------------------------------------------------------

var app = builder.Build();

app.MapControllers();

app.Run();

// ------------------------------------------------------------------------------------------------
