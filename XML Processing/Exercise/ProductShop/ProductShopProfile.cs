namespace ProductShop
{
    using AutoMapper;
    using ProductShop.DTOs;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.Buyer, y => y.MapFrom(p => $"{p.Buyer.FirstName} {p.Buyer.LastName}"));
        }
    }
}
