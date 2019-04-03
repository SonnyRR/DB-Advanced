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
            var gamesDto = JsonConvert.DeserializeObject<List<GameDto>>(jsonString);

            StringBuilder builder = new StringBuilder();

            //var gamesFiltered = gamesDto
            //    .Where(g => IsValid(g) == true)
            //    .ToList();
            //
            //var invalidGamesCount = gamesDto.Except(gamesFiltered).Count();
            //builder.Append(string.Join(Environment.NewLine, Enumerable.Repeat($"Invalid data", invalidGamesCount)));

            var mappedGames = new List<Game>();

            foreach (var g in gamesDto)
            {
                if (IsValid(g) == false)
                {
                    builder.AppendLine("Invalid Data");
                    continue;
                }

                Game game = new Game();

                var genre = GetObjectFromSet<Genre>(x => x.Name == g.Genre, context);
                var developer = GetObjectFromSet<Developer>(x => x.Name == g.Developer, context);

                genre = genre ?? new Genre() { Name = g.Genre };
                developer = developer ?? new Developer() { Name = g.Developer };

                game.Name = g.Name;
                game.Price = g.Price;
                game.Developer = developer;
                game.Genre = genre;
                game.ReleaseDate = g.ReleaseDate;

                foreach (var tag in g.Tags.Distinct())
                {
                    Tag currentTag = GetObjectFromSet<Tag>(x => x.Name == tag, context);
                    currentTag = currentTag ?? new Tag() { Name = tag };

                    game.GameTags.Add(new GameTag() { Game = game, Tag = currentTag });
                }

                mappedGames.Add(game);
                builder.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");

                context.Games.Add(game);
                context.SaveChanges();
            };


            return builder.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets an object instance from DbSet T
        /// </summary>
        /// <returns>The object from set.</returns>
        /// <param name="predicate">Predicate.</param>
        /// <param name="context">Context.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static T GetObjectFromSet<T>(Func<T, bool> predicate, VaporStoreDbContext context) //object[] propertyValuesToSearch, string[] propertyNames, VaporStoreDbContext context)
            where T : class, new()
        {
            var dbSetType = context.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(DbSet<T>)));

            if (dbSetType == null)
                throw new ArgumentException($"DbSet with type: {dbSetType.Name} does not exist!");

            //var currentPropertyNames = typeof(T)
            //    .GetProperties()
            //    .Select(pn => pn.Name)
            //    .ToArray();

            //var propertiesThatDoNotMatch = propertyNames
            //    .Except(currentPropertyNames)
            //    .ToArray();

            //if (propertiesThatDoNotMatch.Length > 0)
            //     throw new ArgumentException($@"Properties with names: ""{string.Join(", ", propertiesThatDoNotMatch)}"" does not exist in class: {typeof(T).Name}!");


            DbSet<T> set = (DbSet<T>)dbSetType
                .GetMethod
                .Invoke(context, null);

            T desiredObject = set
                //.Where(x => x.GetType().GetProperties().IsEntityEqual(propertyNames, propertyValuesToSearch, x) == true)
                .FirstOrDefault(predicate);

            return desiredObject;
        }


        [Obsolete]
        private static bool IsEntityEqual(this PropertyInfo[] properties, string[] propertyNames, object[] values, object obj)
        {
            bool isEqual = true;

            properties = properties.Where(p => propertyNames.Contains(p.Name)).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                if ((string)properties[i].GetValue(obj) != (string)values[i])
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }
    }
}