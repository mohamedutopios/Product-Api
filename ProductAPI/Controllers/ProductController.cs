using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Policy = "ApiScope")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.UpdateProductAsync(updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}