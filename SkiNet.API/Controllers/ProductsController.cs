using Microsoft.AspNetCore.Mvc;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repository.AddProduct(product);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await repository.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductBy(int id)
        {
            var product = await repository.GetProductByIdAsync(id);

            if (product is null) 
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var productToUpdate = await repository.GetProductByIdAsync(id);

            if (!ProductExists(id) || productToUpdate is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.PictureUrl = product.PictureUrl;
            productToUpdate.Type = product.Type;
            productToUpdate.Brand = product.Brand;
            productToUpdate.QuantityInStock = product.QuantityInStock;

            await repository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await repository.GetProductByIdAsync(id);

            if (!ProductExists(id) || productToDelete is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            repository.DeleteProduct(productToDelete);

            await repository.SaveChangesAsync();

            return Ok();
        }

        private bool ProductExists(int id)
        {
            return repository.ProductExists(id);
        }
    }
}
