namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.DTOs;

    public static class Deserializer
    {
        private static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, validateAllProperties: true);

            return isValid;
        }

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var games = JsonConvert.DeserializeObject<List<GameDto>>(jsonString);

            StringBuilder builder = new StringBuilder();

            games = games
                .Where(g => IsValid(g))
                .ToList();

            games.ForEach(g =>
            {
                Game game = new Game();
                var a = GetObjectFromSet<Genre>(new object[] { "FPS" }, new string[] { "Name" }, context);

            });


            return null;
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        private static T GetObjectFromSet<T>(object[] propertyValuesToSearch, string[] propertyNames, VaporStoreDbContext context)
            where T : class, new()
        {
            var dbSetType = context.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(DbSet<T>)));

            if (dbSetType == null)
                throw new ArgumentException($"DbSet with type: {dbSetType.Name} does not exist!");

            var currentPropertyNames = typeof(T)
                .GetProperties()
                .Select(pn => pn.Name)
                .ToArray();

            var propertiesThatDoNotMatch = propertyNames
                .Except(currentPropertyNames)
                .ToArray();

            if (propertiesThatDoNotMatch.Length > 0)
                throw new ArgumentException($@"Properties with names: ""{string.Join(", ", propertiesThatDoNotMatch)}"" does not exist in class: {typeof(T).Name}!");


            DbSet<T> set = (DbSet<T>)dbSetType
                .GetMethod
                .Invoke(context, null);

            T desiredObject = null;


            var testSet = set.ToList();

            desiredObject = set
                .Where(x => x.GetType().GetProperties().IsEntityEqual(propertyNames, propertyValuesToSearch, x) == true)
                .FirstOrDefault();

            if (desiredObject == null)
            {

            }

            return null;
        }

        private static bool IsEntityEqual(this PropertyInfo[] properties, string[] propertyNames, object[] values, object obj)
        {
            bool isEqual = true;

            properties = properties.Where(p => propertyNames.Contains(p.Name)).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetValue(obj) != values[i])
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }
    }
}