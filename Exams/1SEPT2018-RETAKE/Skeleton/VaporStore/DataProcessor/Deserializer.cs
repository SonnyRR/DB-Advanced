namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml.Linq;

    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Enums;
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

                builder.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");

                context.Games.Add(game);
                context.SaveChanges();
            };


            return builder.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDto = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);

            StringBuilder builder = new StringBuilder();

            foreach (var ud in usersDto)
            {
                if (IsValid(ud) == false)
                {
                    builder.AppendLine("Invalid Data");
                    continue;
                }

                User currentUser = new User()
                {
                    FullName = ud.FullName,
                    Username = ud.Username,
                    Email = ud.Email,
                    Age = ud.Age
                };

                foreach (var cd in ud.Cards)
                {
                    Card currentCard = new Card()
                    {
                        Cvc = cd.Cvc,
                        Number = cd.Number,
                        Type = (CardType)Enum.Parse(typeof(CardType), cd.Type, ignoreCase: true)
                    };

                    currentUser.Cards.Add(currentCard);
                }

                builder.AppendLine($"Imported {currentUser.Username} with {currentUser.Cards.Count} cards");

                context.Users.Add(currentUser);
                context.SaveChanges();
            }

            return builder.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            XDocument document = XDocument.Parse(xmlString);
            string dateFormat = @"dd/MM/yyyy HH:mm";

            var elements = document.Root.Elements().ToList();

            StringBuilder builder = new StringBuilder();

            foreach (var element in elements)
            {
                var currentPurchaseDto = new PurchaseDto()
                {
                    Title = element.Attribute("title").Value,
                    Type = element.Element("Type").Value,
                    ProductKey = element.Element("Key").Value,
                    Date = DateTime.ParseExact(element.Element("Date").Value, dateFormat, CultureInfo.InvariantCulture),
                    Card = element.Element("Card").Value
                };

                PurchaseType currentPurchaseType;

                bool isTypeParseSuccessfull = Enum.TryParse<PurchaseType>(currentPurchaseDto.Type, out currentPurchaseType);

                if (IsValid(currentPurchaseDto) == false || isTypeParseSuccessfull == false)
                    continue;

                Purchase currentPurchase = new Purchase()
                {
                    Date = currentPurchaseDto.Date,
                    Type = currentPurchaseType
                };

                var currentGame = GetObjectFromSet<Game>(x => x.Name == currentPurchaseDto.Title, context);

                if (currentGame == null)
                    continue;

                var currentCard = GetObjectFromSet<Card>(x => x.Number == currentPurchaseDto.Card, context);

                if (currentCard == null)
                    continue;

                currentPurchase.Game = currentGame;
                currentPurchase.Card = currentCard;

                string currentUsernameOfBuyer =
                    GetObjectFromSet<User>(x => x.Cards.Any(z => z.Number == currentCard.Number), context).Username;

                context.Purchases.Add(currentPurchase);
                builder.AppendLine($"Imported {currentGame.Name} for {currentUsernameOfBuyer}");
            }

            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }


        /// <summary>
        /// Gets an object instance from DbSet T
        /// </summary>
        /// <returns>The object from set.</returns>
        /// <param name="predicate">Predicate.</param>
        /// <param name="context">Context.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static T GetObjectFromSet<T>(Func<T, bool> predicate, VaporStoreDbContext context)
            where T : class, new()
        {
            var dbSetType = context.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(DbSet<T>)));

            if (dbSetType == null)
                throw new ArgumentException($"DbSet with type: {dbSetType.Name} does not exist!");

            DbSet<T> set = (DbSet<T>)dbSetType
                .GetMethod
                .Invoke(context, null);

            T desiredObject = set
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