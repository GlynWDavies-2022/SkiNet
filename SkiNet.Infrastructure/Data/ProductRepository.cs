using Microsoft.EntityFrameworkCore;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.Infrastructure.Data;

public class ProductRepository(SkiNetContext context) : IProductRepository
{

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }
    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
