using Microsoft.EntityFrameworkCore;
using SkiNet.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<SkiNetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkiNetDatabaseConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
