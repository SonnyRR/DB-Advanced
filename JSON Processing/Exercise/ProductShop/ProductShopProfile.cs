using AutoMapper;
using ProductShop.Export;
using ProductShop.Models;
using System.Collections.Generic;

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


            CreateMap<User, UserDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(obj => obj));

            CreateMap<User, SoldProducts>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.ProductsSold));

            CreateMap<Product, ProductsDto>();

            //CreateMap<UserDto, UsersAndProductsDto>()
            //    .ForMember(x => x.Users, y => y.MapFrom(obj => obj));

            CreateMap<List<UserDto>, UsersAndProductsDto>()
                .ForMember(x => x.Users, y => y.MapFrom(obj => obj));
        }
    }
}
