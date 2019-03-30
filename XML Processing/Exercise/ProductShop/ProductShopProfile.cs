namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductShop.DTOs;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {

            // 05 Ex mappings
            CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.Buyer, y => y.MapFrom(p => $"{p.Buyer.FirstName} {p.Buyer.LastName}"));

            // 06 Ex mappings
            CreateMap<User, GetSoldProductsDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(obj => obj.ProductsSold));

            CreateMap<Product, SoldProductDto>();           
        }
    }
}
