using System.Linq;
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

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();

        if(!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(p => p.Brand.Equals(brand));
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(p => p.Type.Equals(type));
        }

        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query
        };

        return await query.ToListAsync();
    }

    // Alternative approach ---------------------------------------------------------------------

    // public async Task<IReadOnlyList<Product>> GetProductsByBrandAsync(string brand)
    // {
    //    return await context.Products.Where<Product>(p => p.Brand.Equals(brand)).ToListAsync();
    // }

    // ------------------------------------------------------------------------------------------

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

    // Brands

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context
            .Products
            .Select(p => p.Brand)
            .Distinct()
            .ToListAsync();
    }

    // Types

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context
            .Products
            .Select(p => p.Type)
            .Distinct()
            .ToListAsync();
    }
}
