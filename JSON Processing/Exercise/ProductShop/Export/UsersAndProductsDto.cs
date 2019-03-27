using System.Collections.Generic;

namespace ProductShop.Export
{
    public class ProductsDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public SoldProducts SoldProducts { get; set; }
    }

    public class SoldProducts
    {
        public SoldProducts()
        {
            this.Products = new List<ProductsDto>();
        }

        public int Count { get => this.Products.Count; }

        public List<ProductsDto> Products { get; set; } 
    }

    public class UsersAndProductsDto
    {
        public UsersAndProductsDto()
        {
            this.Users = new List<UserDto>();
        }

        public int UsersCount { get => this.Users.Count;}

        public List<UserDto> Users { get; set; }
    }
}
