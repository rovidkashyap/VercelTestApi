using Microsoft.AspNetCore.Mvc;
using VercelTestApi.Models;
using VercelTestApi.Repository.ProductRepository;

namespace VercelTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var products = await _productRepository.GetByIdAsync(id);
            if(products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if(existingProduct == null)
            {
                return NotFound();
            }

            // Updating Properties
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductPrice = product.ProductPrice;
            existingProduct.InStock = product.InStock;
            existingProduct.CategoryId = product.CategoryId;

            await _productRepository.UpdateAsync(existingProduct);
            await _productRepository.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveAsync();

            return NoContent();
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            if(!products.Any())
            {
                return NotFound(new { message = "No Products Found for the specified category." });
            }

            return Ok(products);
        }
    }
}
