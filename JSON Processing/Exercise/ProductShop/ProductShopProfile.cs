using AutoMapper;
using ProductShop.Export;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<Product, ProductsInRangeDto>()
                .ForMember(x => x.Seller, y => y.MapFrom(p => $"{p.Seller.FirstName} {p.Seller.LastName}"));
        }
    }
}
