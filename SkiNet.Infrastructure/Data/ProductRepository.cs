using Microsoft.EntityFrameworkCore;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;
using System.Runtime.CompilerServices;

namespace SkiNet.Infrastructure.Data;

public class ProductRepository(SkiNetContext context) : IProductRepository
{
    private readonly SkiNetContext _context = context;

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }
    public bool ProductExists(int id)
    {
        return _context.Products.Any(p => p.Id == id);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
