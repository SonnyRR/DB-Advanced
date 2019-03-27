namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.DTO;
    using CarDealer.Models;
    using Newtonsoft.Json;


    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new CarDealerProfile()));

            Insert();
        }

        private static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }

        public static void Insert()
        {
            string carsPath = @"../../../Datasets/cars.json";
            string customersPath = @"../../../Datasets/customers.json";
            string partsPath = @"../../../Datasets/parts.json";
            string salesPath = @"../../../Datasets/sales.json";
            string suppliersPath = @"../../../Datasets/suppliers.json";

            if (File.Exists(carsPath))
            {
                var importData = File.ReadAllText(carsPath);

                using (var context = new CarDealerContext())
                {
                    string output = ImportCars(context, importData);
                    Console.WriteLine(output);
                }

            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var existingSuppliers = context.Suppliers
                .Select(s => s.Id)
                .ToArray();

            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => existingSuppliers.Contains(p.SupplierId))
                .ToArray();

            context.Parts.AddRange(parts);
            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<CarInsertDto[]>(inputJson);
            var mappedCars = new List<Car>();
            int affectedRows = 0;

            foreach (var car in cars)
            {
                Car vehicle = Mapper.Map<CarInsertDto, Car>(car);
                mappedCars.Add(vehicle);
            }

            affectedRows += context.SaveChanges();
            mappedCars = context.Cars.ToList();

            var parts = new List<PartCar>();

            foreach (var car in mappedCars)
            {
                var partIds = cars.FirstOrDefault(x => x.Make == car.Make
                && x.Model == car.Model && x.TravelledDistance == car.TravelledDistance)
                .PartsId
                .ToList();

                if (partIds == null)
                    continue;

                partIds.ForEach(pid =>
                {
                    var currentPair = new PartCar()
                    {
                        Car = car,
                        PartId = pid
                    };

                    parts.Add(currentPair);
                }
                );
            }

            context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            int affectedRows = context.SaveChanges();

            return $"Successfully imported {affectedRows}.";
        }
    }
}