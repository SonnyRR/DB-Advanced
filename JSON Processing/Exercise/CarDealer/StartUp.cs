namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.Models;
    using Newtonsoft.Json;


    public class StartUp
    {
        public static void Main()
        {
            
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

            if (File.Exists(""))
            {
                var ImportData = File.ReadAllText();

                using (var context = new CarDealerContext())
                {
                    string output;
                    Console.WriteLine(output);
                }

            }
        }
}