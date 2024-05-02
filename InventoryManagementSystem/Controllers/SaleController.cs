using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<SaleDto>>>> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<SaleDto>>> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SaleDto>>> CreateSale(SaleDto saleDto)
        {
            var createdSale = await _saleService.CreateSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.Data.Id }, createdSale);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<SaleDto>>> UpdateSale(int id, SaleDto saleDto)
        {
            var updatedSale = await _saleService.UpdateSaleAsync(id, saleDto);
            if (updatedSale.Data == null)
            {
                return NotFound(updatedSale);
            }
            return Ok(updatedSale);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteSale(int id)
        {
            var deleteResponse = await _saleService.DeleteSaleAsync(id);
            if (!deleteResponse.Data)
            {
                return NotFound(deleteResponse);
            }
            return Ok(deleteResponse);
        }
    }
}
