namespace ProductShop.DTOs
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    //[XmlType("User")]
    public class UsersAndProductsDto
    {
        public UsersAndProductsDto()
        {
            this.Users = new List<UserDto>();
        }

        [XmlElement("count")]
        public int Count { get; set; }


        [XmlArray("users")]
        public List<UserDto> Users { get; set; }
    }

    [XmlType("User")]
    public class UserDto
    {

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int Age { get; set; }

        public SoldProductsFacade SoldProducts { get; set; }
    }

    //[XmlType("Product")]
    //public class ProductDto
    //{
    //    [XmlElement("name")]
    //    public string Name { get; set; }
    //
    //    [XmlElement("price")]
    //    public decimal Price { get; set; }
    //}

    [XmlType("SoldProducts")]
    public class SoldProductsFacade
    {
        public SoldProductsFacade()
        {
            this.Products = new List<SoldProductDto>();
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<SoldProductDto> Products { get; set; }
    }
}
