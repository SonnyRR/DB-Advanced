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

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new CarDealerProfile()));

            InsertStatment();
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

        public static T DeserializeObject<T>(string values, string rootName)
        {
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

            T deserializedObject = (T)serializer.Deserialize(new StringReader(values));

            return deserializedObject;

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
                //string result = GetUsersWithProducts(context);
                //Console.WriteLine(result);
            }
        }

        public static void InsertStatment()
        {
            string customersXmlPath = @"../../../Datasets/customers.xml";
            string partsXmlPath = @"../../../Datasets/parts.xml";
            string salesXmlPath = @"../../../Datasets/sales.xml";
            string suppliersXmlPath = @"../../../Datasets/suppliers.xml";
            string carsXmlPath = @"../../../Datasets/cars.xml";

            if (File.Exists(suppliersXmlPath))
            {
                var importData = File.ReadAllText(suppliersXmlPath);

                using (var context = new CarDealerContext())
                {
                    string output = ImportSuppliers(context, importData);
                    Console.WriteLine(output);
                }
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {

            var suppliers = DeserializeObject<Supplier[]>(inputXml, "Suppliers");

            int affectedRows = context.SaveChanges();
            return $"Successfully imported {affectedRows}";
        }
    }
}