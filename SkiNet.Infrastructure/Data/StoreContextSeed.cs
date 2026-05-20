using SkiNet.Core.Entities;
using System.Text.Json;

namespace SkiNet.Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(SkiNetContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../SkiNet.Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null)
            {
                return;
            }

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}
