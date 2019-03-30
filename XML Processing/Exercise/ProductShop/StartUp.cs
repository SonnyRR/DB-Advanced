namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using ProductShop.Data;
    using ProductShop.DTOs;
    using ProductShop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

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
                string result = GetUsersWithProducts(context);
                Console.WriteLine(result);
            }
        }

        public static string SerializeObject<T>(T values, string rootName, bool omitXmlDeclaration = false, 
            bool indentXml = true)
        {
            string xml = string.Empty;

            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

            var settings = new XmlWriterSettings()
            {
                Indent = indentXml,
                OmitXmlDeclaration = omitXmlDeclaration
            };

            XmlSerializerNamespaces @namespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, values, @namespace);
                xml = stream.ToString();
            }

            return xml;
        }

        private static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }

        public static void InsertStatment()
        {
            string usersXmlPath = @"../../../Datasets/users.xml";
            string productsXmlPath = @"../../../Datasets/products.xml";
            string categoriesXmlPath = @"../../../Datasets/categories.xml";
            string categoriesProductsXmlPath = @"../../../Datasets/categories-products.xml";

            if (File.Exists(usersXmlPath))
            {
                var importData = File.ReadAllText(categoriesProductsXmlPath);

                using (ProductShopContext context = new ProductShopContext())
                {
                    string output = ImportCategoryProducts(context, importData);
                    Console.WriteLine(output);
                }
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Load(inputXml);
            var usersFromXml = doc.Root
                .Elements()
                .ToList();

            var users = new List<User>();

            usersFromXml.ForEach(x =>
            {
                User currentUser = new User();
                currentUser.FirstName = x.Element("firstName").Value;
                currentUser.LastName = x.Element("lastName").Value;
                currentUser.Age = Convert.ToInt32(x.Element("age").Value);

                users.Add(currentUser);
            });

            context.Users.AddRange(users);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var productsFromXml = doc.Root
                .Elements()
                .ToList();

            var products = new List<Product>();

            productsFromXml.ForEach(x =>
            {
                Product currentProduct = new Product();
                currentProduct.Name = x.Element("name").Value;
                currentProduct.Price = Convert.ToDecimal(x.Element("price").Value);

                var sellerId = Convert.ToInt32(x.Element("sellerId").Value);
                var buyerId = Convert.ToInt32(x.Element("sellerId").Value);

                currentProduct.SellerId = sellerId;
                currentProduct.BuyerId = buyerId == 0 ? null : (int?)buyerId;

                products.Add(currentProduct);
            });

            context.Products.AddRange(products);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var categoriesFromXml = doc.Root
                .Elements()
                .ToList();

            var categories = new List<Category>();

            categoriesFromXml.ForEach(x =>
            {
                Category currentCategory = new Category();

                currentCategory.Name = x.Element("name").Value;
                categories.Add(currentCategory);
            });

            context.Categories.AddRange(categories);
            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XDocument doc = XDocument.Parse(inputXml);

            var categoryProductsFromXml = doc.Root
                .Elements()
                .ToList();

            List<CategoryProduct> pairs = new List<CategoryProduct>();

            foreach (var cp in categoryProductsFromXml)
            {
                CategoryProduct currentPair = new CategoryProduct();

                var categoryId = Convert.ToInt32(cp.Element("CategoryId").Value);
                var productId = Convert.ToInt32(cp.Element("ProductId").Value);

                if (categoryId == 0 || productId == 0)
                    continue;

                currentPair.CategoryId = categoryId;
                currentPair.ProductId = productId;
                pairs.Add(currentPair);
            };

            pairs = pairs.Distinct().ToList();
            context.CategoryProducts.AddRange(pairs);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Include(x => x.Buyer)
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Take(10)
                .ProjectTo<ProductInRangeDto>()
                .ToList();

            var xml = SerializeObject(products, "Product", false);
            return xml;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(u => u.ProductsSold)
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ProjectTo<GetSoldProductsDto>()
                .ToList();

            var xml = SerializeObject(users, "Users");
            return xml;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .ProjectTo<CategoriesByProductsDto>()
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            var xml = SerializeObject(categories, "Categories");
            return xml;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(x => x.ProductsSold)
                .Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ProjectTo<UserDto>()
                .ToList();

            var facade = Mapper.Map<UsersAndProductsDto>(users.ToList());

            var xml = SerializeObject(facade, "Users");
            return xml;
        }
    }
}