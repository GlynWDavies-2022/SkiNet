using Microsoft.EntityFrameworkCore;
using SkiNet.Core.Entities;

namespace SkiNet.Infrastructure.Data;

public class SkiNetContext(DbContextOptions options) : DbContext(options)
{ 
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
