using System;

namespace ProductShop.Models
{
    public class CategoryProduct : IEquatable<CategoryProduct>
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public bool Equals(CategoryProduct other)
        {
            if (this.CategoryId == other.CategoryId && this.ProductId == other.ProductId)
                return true;

            else
                return false;
        }
    }
}
