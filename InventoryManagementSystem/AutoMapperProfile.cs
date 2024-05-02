using AutoMapper;
using InventoryManagementSystem.Dtos;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
        }
    }
}
