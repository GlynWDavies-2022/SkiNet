using SkiNet.Core.Entities;

namespace SkiNet.Core.Interfaces;

public interface IProductRepository
{
    public void AddProduct(Product product);
    public Task<IReadOnlyList<Product>> GetProductsAsync();
    public Task<Product?> GetProductByIdAsync(int id);
    public void UpdateProduct(Product product);
    public void DeleteProduct(Product product);
    public bool ProductExists(int id);
    public Task<bool> SaveChangesAsync();
}
