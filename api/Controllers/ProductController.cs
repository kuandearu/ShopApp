using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Product;
using api.Models;
using api.Repositories.Products;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDTOs = products.Select(p => p.ToProductDTO());
            return Ok(productDTOs);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDTO());
        }

        // POST: api/Product
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductResponseDTO createProductDto)
        {
            // Mapping DTO to model
            var product = createProductDto.ToCreateProductResponseDTO();
            var createdProduct = await _productRepository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct.ToProductDTO());
        }

        // PUT: api/Product/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductResponseDTO updateProductDto)
        {
            // Mapping DTO to model
            var product = updateProductDto.ToUpdateProductResponseDTO();
            var updatedProduct = await _productRepository.UpdateProductAsync(product, id);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct.ToProductDTO());
        }

        // DELETE: api/Product/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _productRepository.DeleteProductAsync(id);
            if (deletedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
