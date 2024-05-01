using AutoMapper;
using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, InventoryDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> CreateProductAsync(ProductDto productDto)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ProductDto>>();
            await _context.Products.AddAsync(_mapper.Map<Product>(productDto));
            await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Products.Select(p => _mapper.Map<ProductDto>(p));
            return serviceResponse;
        }


        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ProductDto>>();
            var dbProducts = await _context.Products.ToListAsync();
            serviceResponse.Data = dbProducts.Select(p => _mapper.Map<ProductDto>(p));
            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<ProductDto>();
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<ProductDto>(dbProduct);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductDto>> UpdateProductAsync(int id, ProductDto productDto)
        {
            var serviceResponse = new ServiceResponse<ProductDto>();
            try
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (dbProduct is null)
                    throw new Exception("Product not found");

                dbProduct.Name = productDto.Name;
                dbProduct.Description = productDto.Description;
                dbProduct.Quantity = productDto.Quantity;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<ProductDto>(dbProduct);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> DeleteProductAsync(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ProductDto>>();
            try
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (dbProduct is null)
                    throw new Exception("Product not found");

                _context.Products.Remove(dbProduct);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Products.Select(p => _mapper.Map<ProductDto>(p));

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
