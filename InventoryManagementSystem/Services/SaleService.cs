using AutoMapper;
using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class SaleService : ISaleService
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public SaleService(IMapper mapper, InventoryDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<SaleDto>>> GetAllSalesAsync()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<SaleDto>>();
            var dbSales = await _context.Sales.ToListAsync();
            var dbProducts = await _context.Products.ToListAsync();
            dbSales.ForEach(sale => sale.Product.Name = dbProducts.FirstOrDefault(p => p.Id == sale.ProductId)?.Name);

            serviceResponse.Data = _mapper.Map<IEnumerable<SaleDto>>(dbSales);
            return serviceResponse;
        }

        public async Task<ServiceResponse<SaleDto>> GetSaleByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<SaleDto>();
            var dbSale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
            serviceResponse.Data = _mapper.Map<SaleDto>(dbSale);
            return serviceResponse;
        }

        public async Task<ServiceResponse<SaleDto>> CreateSaleAsync(SaleDto saleDto)
        {
            var serviceResponse = new ServiceResponse<SaleDto>();
            var sale = _mapper.Map<Sale>(saleDto);
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<SaleDto>(sale);
            return serviceResponse;
        }

        public async Task<ServiceResponse<SaleDto>> UpdateSaleAsync(int id, SaleDto saleDto)
        {
            var serviceResponse = new ServiceResponse<SaleDto>();
            try
            {
                var dbSale = await _context.Sales.FirstOrDefaultAsync(s => s.ProductId == id);
                if (dbSale is null)
                    throw new Exception("Sale not found");

                dbSale.Date = DateTime.Now;
                dbSale.QuantitySold += saleDto.QuantitySold;                

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<SaleDto>(dbSale);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteSaleAsync(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var dbSale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
                if (dbSale is null)
                    throw new Exception("Sale not found");

                _context.Sales.Remove(dbSale);
                await _context.SaveChangesAsync();
                serviceResponse.Data = true;
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
