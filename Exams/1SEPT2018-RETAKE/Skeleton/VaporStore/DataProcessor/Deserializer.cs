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
                var a = GetObjectFromSet<Developer>("a", "Name34", context);

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

        private static T GetObjectFromSet<T>(string propertyValueToSearch, string destinationProperty, VaporStoreDbContext context)
            where T : class, new()
        {
            var dbSetType = context.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(DbSet<T>)));

            if (dbSetType == null)
                throw new ArgumentException($"DbSet with type: {dbSetType.Name} does not exist!");

            var TProperty = typeof(T).GetProperty(destinationProperty, BindingFlags.Instance | BindingFlags.Public);

            if (TProperty == null)
                throw new ArgumentException($"Property with with type: {destinationProperty} does not exist in class:{typeof(T).Name}!");


            var set = (DbSet<T>)dbSetType
                .GetMethod
                .Invoke(context, null);

            T desiredObject = null;

            desiredObject = set
                .Where(x => (string)x.GetType().GetProperty(destinationProperty).GetValue(x) == propertyValueToSearch)
                .FirstOrDefault();

            if (desiredObject == null)
            {

            }

            return null;
        }
    }
}