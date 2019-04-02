namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.DTOs;

    public static class Deserializer
    {
        private static bool IsValid(object @object)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, true);

            return isValid;
        }

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var games = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            StringBuilder builder = new StringBuilder();

            var validGames = new List<Game>();

            foreach (var game in games)
            {
                if (string.IsNullOrWhiteSpace(game.Name) || game.ReleaseDate == null 
                    || game.Price < 0.00M || game.Genre == null || game.Developer == null 
                    || game.Tags.Length == 0)
                {
                    builder.AppendLine($"Invalid Data{Environment.NewLine}");
                    continue;
                }

                
            }

            context.Games.AddRange(validGames);

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
    }
}