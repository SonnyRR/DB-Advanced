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

            CreateMap<User, UserProductsSellerDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(u => u.ProductsSold));

            CreateMap<Product, UserSoldProductsDto>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(p => p.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(p => p.Buyer.LastName));
        }
    }
}
