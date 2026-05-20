using Microsoft.AspNetCore.Mvc;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        private readonly IProductRepository _repository = repository;

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repository.AddProduct(product);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await _repository.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductBy(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var productToUpdate = await _repository.GetProductByIdAsync(id);

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

            await _repository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _repository.GetProductByIdAsync(id);

            if (!ProductExists(id) || productToDelete is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            _repository.DeleteProduct(productToDelete);

            await _repository.SaveChangesAsync();

            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _repository.ProductExists(id);
        }
    }
}
