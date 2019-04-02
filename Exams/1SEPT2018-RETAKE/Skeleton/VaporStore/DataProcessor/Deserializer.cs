namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
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
            var currentDevelopers = new List<Developer>();
            var currentGenres = new List<Genre>();
            var currentTags = new List<Tag>();

            foreach (var game in games)
            {
                if (string.IsNullOrWhiteSpace(game.Name) || game.ReleaseDate == null
                    || game.Price < 0.00M || game.Genre == null || game.Developer == null
                    || game.Tags.Length == 0)
                {
                    builder.AppendLine($"Invalid Data{Environment.NewLine}");
                    continue;
                }

                Developer currentDev = null;

                if (currentDevelopers.Any(x => x.Name == game.Developer) == false)
                {
                    currentDev = new Developer() { Name = game.Developer };
                    currentDevelopers.Add(currentDev);
                }

                else
                {
                    currentDev = currentDevelopers.First(x => x.Name == game.Developer);
                }

                Genre currentGenre = null;

                if (currentGenres.Any(x => x.Name == game.Genre) == false)
                {
                    currentGenre = new Genre() { Name = game.Genre };
                    currentGenres.Add(currentGenre);
                }

                else
                {
                    currentGenre = currentGenres.First(x => x.Name == game.Genre);
                }                

                Game currentGame = new Game()
                {
                    Name = game.Name,
                    Developer = currentDev,
                    Genre = currentGenre,
                    ReleaseDate = game.ReleaseDate,
                };

                foreach (var tag in game.Tags.Distinct())
                {
                    Tag currentTag = null;
                    if (currentTags.Any(x => x.Name == tag) == false)
                    {
                        currentTag = new Tag() { Name = tag };
                        currentTags.Add(currentTag);
                    }

                    else
                    {
                        currentTag = currentTags.First(x => x.Name == tag);
                    }                    
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