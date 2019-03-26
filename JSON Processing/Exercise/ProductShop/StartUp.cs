using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Export;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new ProductShopProfile()));
            QueryAndExport();
        }

        public static void QueryAndExport()
        {
            using (ProductShopContext context = new ProductShopContext())
            {
                string result = GetProductsInRange(context);
                Console.WriteLine(result);
            }
        }

        public static void InsertStatment()
        {
            string usersJsonPath = @"../../../ Datasets/users.json";
            string productsJsonPath = @"../../../Datasets/products.json";
            string categoriesJsonPath = @"../../../Datasets/categories.json";
            string categoriesProductsPath = @"../../../Datasets/categories-products.json";

            if (File.Exists(categoriesProductsPath))
            {
                var ImportData = File.ReadAllText(categoriesProductsPath);

                using (ProductShopContext context = new ProductShopContext())
                {
                    var output = ImportCategoryProducts(context, ImportData);
                    Console.WriteLine(output);
                }
            }

        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson)
                .Where(x => x.LastName.Length >= 3)
                .ToList();

            context.Users.AddRange(users);
            var affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];

                if (product.Name.Length < 3 || product.Price == 0.0M || product.SellerId < 1)
                {
                    product = null;
                }
            }

            context.Products.AddRange(products.Where(x => x != null));
            var affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null)
                .ToArray();

            for (int i = 0; i < categories.Length; i++)
            {
                var category = categories[i];

                if (category.Name.Length < 3 || category.Name.Length > 15)
                {
                    category = null;
                }
            }

            context.Categories.AddRange(categories.Where(x => x != null));
            var affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            var affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            List<ProductsInRangeDto> products = context.Products
                .Where(x => x.Price >= 500M && x.Price <= 1000M)
                .ProjectTo<ProductsInRangeDto>()
                .ToList();

            string json = JsonConvert.SerializeObject(products);
            return json;
        }
    }
}