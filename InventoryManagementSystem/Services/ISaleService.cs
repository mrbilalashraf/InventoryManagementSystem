using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Services
{
    public interface ISaleService
    {
        Task<ServiceResponse<IEnumerable<SaleDto>>> GetAllSalesAsync();
        Task<ServiceResponse<SaleDto>> GetSaleByIdAsync(int id);
        Task<ServiceResponse<SaleDto>> CreateSaleAsync(SaleDto saleDto);
        Task<ServiceResponse<SaleDto>> UpdateSaleAsync(int id, SaleDto saleDto);
        Task<ServiceResponse<bool>> DeleteSaleAsync(int id);
    }
}
