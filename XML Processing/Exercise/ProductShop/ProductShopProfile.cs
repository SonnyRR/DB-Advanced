namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductShop.DTOs;
    using ProductShop.Models;
    using System.Collections.Generic;
    using System.Linq;

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

            // 07 Ex mappings
            CreateMap<Category, CategoriesByProductsDto>()
                .ForMember(x => x.Count, y => y.MapFrom(obj => obj.CategoryProducts.Count))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(obj => obj.CategoryProducts.Sum(z => z.Product.Price)))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(obj => obj.CategoryProducts.Average(z => z.Product.Price)));

            // 08 Ex mappings
            CreateMap<ICollection<UserDto>, UsersAndProductsDto>()
                .ForMember(x => x.Users, y => y.MapFrom(obj => obj.Take(10)))
                .ForMember(x => x.Count, y => y.MapFrom(obj => obj.Count));

            CreateMap<User, UserDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(obj => obj.ProductsSold));

            CreateMap<User, SoldProductsFacade>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.ProductsSold));

            CreateMap<ICollection<Product>, SoldProductsFacade>()
                .ForMember(x => x.Products, y => y.MapFrom(obj => obj.OrderByDescending(z => z.Price)))
                .ForMember(x => x.Count, y => y.MapFrom(obj => obj.Count));
        }
    }
}
