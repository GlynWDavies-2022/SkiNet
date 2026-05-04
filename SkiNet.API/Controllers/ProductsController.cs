using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiNet.Core.Entities;
using SkiNet.Infrastructure.Data;

namespace SkiNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SkiNetContext _context;

        public ProductsController(SkiNetContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductBy(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (!ProductExists(id) || product == null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var productToUpdate = await _context.Products.FindAsync(id);

            if (!ProductExists(id) || productToUpdate is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (!ProductExists(id) || productToDelete is null)
            {
                return NotFound($"Product with id {id} could not be found!");
            }

            _context.Products.Remove(productToDelete);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }
    }
}
