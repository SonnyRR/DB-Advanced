namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Xml.Linq;
    using System.Linq;
    using System.Globalization;
    using CarDealer.Dtos.Export;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new CarDealerProfile()));

            QueryAndExport();
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

        //TODO
        [Obsolete]
        public static ICollection<T> DeserializeObject<T>(string plainXml, string rootName)
        {
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));


            XDocument doc = XDocument.Parse(plainXml);
            var elements = doc.Elements();

            T destinationObject = Activator.CreateInstance<T>();
            var destObjProperties = typeof(T).GetProperties();

            foreach (var property in destObjProperties)
            {

            }


            T deserializedObject = (T)serializer.Deserialize(new StringReader(plainXml));

            return null;

        }

        private static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }

        public static void QueryAndExport()
        {
            using (var context = new CarDealerContext())
            {
                var xml = GetTotalSalesByCustomer(context);
                Console.WriteLine(xml);
            }
        }

        public static void InsertStatment()
        {
            string customersXmlPath = @"../../../Datasets/customers.xml";
            string partsXmlPath = @"../../../Datasets/parts.xml";
            string salesXmlPath = @"../../../Datasets/sales.xml";
            string suppliersXmlPath = @"../../../Datasets/suppliers.xml";
            string carsXmlPath = @"../../../Datasets/cars.xml";

            if (File.Exists(salesXmlPath))
            {
                var importData = File.ReadAllText(salesXmlPath);

                using (var context = new CarDealerContext())
                {
                    string output = ImportSales(context, importData);
                    Console.WriteLine(output);
                }
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {

            var suppliersParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var suppliers = new List<Supplier>();

            suppliersParsed.ForEach(x =>
            {
                Supplier currentSupplier = new Supplier();
                currentSupplier.Name = x.Element("name").Value;
                currentSupplier.IsImporter = bool.Parse(x.Element("isImporter").Value);

                suppliers.Add(currentSupplier);
            });

            context.Suppliers.AddRange(suppliers);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partsParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToList();

            var parts = new List<Part>();

            foreach (var x in partsParsed)
            {
                Part currentPart = new Part()
                {
                    Name = x.Element("name").Value,
                    Price = Convert.ToDecimal(x.Element("price").Value),
                    SupplierId = Convert.ToInt32(x.Element("supplierId").Value),
                    Quantity = Convert.ToInt32(x.Element("quantity").Value)
                };

                if (supplierIds.Contains(currentPart.SupplierId) == false)
                {
                    continue;
                }

                parts.Add(currentPart);
            };

            context.Parts.AddRange(parts);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var cars = new List<Car>();

            var existingPartsIds = context.Parts
                .Select(x => x.Id)
                .ToArray();

            foreach (var x in carsParsed)
            {
                Car currentCar = new Car()
                {
                    Make = x.Element("make").Value,
                    Model = x.Element("model").Value,
                    TravelledDistance = Convert.ToInt64(x.Element("TraveledDistance").Value)
                };

                var partIds = new HashSet<int>();

                foreach (var id in x.Element("parts").Elements())
                {
                    var pid = Convert.ToInt32(id.Attribute("id").Value);
                    partIds.Add(pid);
                }

                foreach (var pid in partIds)
                {
                    PartCar currentPair = new PartCar()
                    {
                        Car = currentCar,
                        PartId = pid
                    };

                    if (existingPartsIds.Contains(pid) == false)
                        continue;

                    currentCar.PartCars.Add(currentPair);
                }

                cars.Add(currentCar);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            int affectedRows = cars.Count;
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var customers = new List<Customer>();

            foreach (var element in customersParsed)
            {
                Customer currentCustomer = new Customer()
                {
                    Name = element.Element("name").Value,
                    IsYoungDriver = bool.Parse(element.Element("isYoungDriver").Value),
                    BirthDate = DateTime.ParseExact(element.Element("birthDate").Value,
                    "yyyy-MM-ddTH:mm:ss", CultureInfo.InvariantCulture)
                };

                customers.Add(currentCustomer);
            }

            context.Customers.AddRange(customers);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var carIds = context.Cars
                .Select(x => x.Id)
                .ToArray();

            var customerIds = context.Customers
                .Select(x => x.Id)
                .ToArray();

            var sales = new List<Sale>();

            foreach (var element in salesParsed)
            {
                var currentCarId = Convert.ToInt32(element.Element("carId").Value);
                var currentCustomerId = Convert.ToInt32(element.Element("customerId").Value);

                if (!carIds.Contains(currentCarId)) //|| !customerIds.Contains(currentCustomerId))
                    continue;

                var currentSale = new Sale()
                {
                    CarId = currentCarId,
                    CustomerId = currentCustomerId,
                    Discount = Convert.ToDecimal(element.Element("discount").Value)
                };


                sales.Add(currentSale);
            }

            context.Sales.AddRange(sales);

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance >= 2_000_000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<CarWithDistanceDto>()
                .ToList();

            var xml = SerializeObject(cars, "cars");
            return xml;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")                
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<BmwCarsDto>()
                .ToList();

            var xml = SerializeObject(cars, "cars");
            return xml;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<LocalSuppliersDto>()
                .ToList();

            var xml = SerializeObject(suppliers, "suppliers");
            return xml;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Include(c => c.PartCars)
                .ThenInclude(pc => pc.Part)
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<CarAndPartsDto>()
                .ToList();

            var xml = SerializeObject(cars, "cars");
            return xml;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Include(x => x.Sales)
                .ThenInclude(x => x.Car)
                .ThenInclude(x => x.PartCars)
                .ThenInclude(x => x.Part)
                //.OrderByDescending(x => x.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price)))
                .ProjectTo<CustomerTotalSale>()
                .OrderByDescending(x => x.SpentMoney)
                .ToList();

            var xml = SerializeObject(customers, "customers");
            return xml;
        }
    }
}