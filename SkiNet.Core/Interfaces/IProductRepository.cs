using SkiNet.Core.Entities;

namespace SkiNet.Core.Interfaces;

public interface IProductRepository
{
    // Products

    public void AddProduct(Product product);
    public Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    
    // Alternative approach ------------------------------------------------------

    // public Task<IReadOnlyList<Product>> GetProductsByBrandAsync(string brand);

    // ---------------------------------------------------------------------------

    public Task<Product?> GetProductByIdAsync(int id);
    public Task UpdateProduct(Product product);
    public void DeleteProduct(Product product);
    public bool ProductExists(int id);
    public Task<bool> SaveChangesAsync();

    // Brands

    public Task<IReadOnlyList<string>> GetBrandsAsync();

    // Types

    public Task<IReadOnlyList<string>> GetTypesAsync();
}
