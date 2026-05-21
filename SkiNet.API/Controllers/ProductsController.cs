using Microsoft.AspNetCore.Mvc;
using SkiNet.Core.Entities;
using SkiNet.Core.Interfaces;

namespace SkiNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repository.Add(product);

            if (await repository.SaveAllAsync())
            {
                return Created();
            }

            return BadRequest("Problem creating product.");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var products = await repository.ListAllAsync();

            return Ok(products);
        }

        // Alternative approach -----------------------------------------------------------------------

        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsByBrandAsync(string brand)
        // {
        //    var products = await repository.GetProductsByBrandAsync(brand);

        //    return Ok(products);
        // }

        // ---------------------------------------------------------------------------------------------

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductBy(int id)
        {
            var product = await repository.GetByIdAsync(id);

            if (product is null) 
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var productToUpdate = await repository.GetByIdAsync(id);

            if (!ProductExists(id) || productToUpdate is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            // productToUpdate.Name = product.Name;
            // productToUpdate.Description = product.Description;
            // productToUpdate.Price = product.Price;
            // productToUpdate.PictureUrl = product.PictureUrl;
            // productToUpdate.Type = product.Type;
            // productToUpdate.Brand = product.Brand;
            // productToUpdate.QuantityInStock = product.QuantityInStock;

            if(await repository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating product.");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await repository.GetByIdAsync(id);

            if (!ProductExists(id) || productToDelete is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            repository.Remove(productToDelete);

            if(await repository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting product.");
        }

        private bool ProductExists(int id)
        {
            return repository.Exists(id);
        }

        // Brands

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
           // Todo...Implement method with generic repository

           return Ok();
        }

        // Types

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            // Todo...Implement method with generic repository

            return Ok();
        }
    }
}
