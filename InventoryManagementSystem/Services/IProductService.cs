using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<ProductDto>>> CreateProductAsync(ProductDto productDto);
        Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, ProductDto productDto);
        Task<ServiceResponse<IEnumerable<ProductDto>>> DeleteProductAsync(int id);
    }
}
