using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> CreateProduct(ProductDto productDto)
        {
            //var createdProduct = await _productService.CreateProductAsync(productDto);
            //return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            return Ok(await _productService.CreateProductAsync(productDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> UpdateProduct(int id, ProductDto productDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
            if (updatedProduct.Data == null)
            {
                return NotFound(updatedProduct);
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> DeleteProduct(int id)
        {
            var updatedProduct = await _productService.DeleteProductAsync(id);
            if (updatedProduct.Data == null)
            {
                return NotFound(updatedProduct);
            }
            return Ok(updatedProduct);
        }
    }
}
