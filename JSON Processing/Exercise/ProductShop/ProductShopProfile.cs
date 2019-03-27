using AutoMapper;
using ProductShop.Export;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            #region Exercise 1.5 mappings

            CreateMap<Product, ProductsInRangeDto>()
                .ForMember(x => x.Seller, y => y.MapFrom(p => $"{p.Seller.FirstName} {p.Seller.LastName}"));
            
            #endregion

            #region Exercise 1.6 mappings

            CreateMap<User, UserProductsSellerDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(u => u.ProductsSold));

            CreateMap<Product, UserSoldProductsDto>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(p => p.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(p => p.Buyer.LastName));

            #endregion

            #region Exercise 1.8 mappings

            CreateMap<User, UserDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(obj => obj));

            CreateMap<User, SoldProducts>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.ProductsSold.Where(x => x.Buyer != null)));

            CreateMap<Product, ProductsDto>();

            CreateMap<List<UserDto>, UsersAndProductsDto>()
                .ForMember(x => x.Users, y => y.MapFrom(obj => obj));

            #endregion
        }
    }
}
