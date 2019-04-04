namespace VaporStore.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Enums;
    using VaporStore.DataProcessor.DTOs.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres
                .Include(x => x.Games)
                    .ThenInclude(x => x.Developer)
                .Include(x => x.Games)
                    .ThenInclude(x => x.Purchases)
                .Include(x => x.Games)
                    .ThenInclude(x => x.GameTags)
                    .ThenInclude(x => x.Tag)
                .Where(g => g.Games.Any(z => z.Purchases.Count > 0) && genreNames.Contains(g.Name))
                .ProjectTo<GenreExportDto>()
                .ToList()
                .OrderByDescending(x => x.Games.Sum(y => y.Players))
                .ThenBy(x => x.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(genres, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var enumParsed = Enum.Parse<PurchaseType>(storeType);

            var purchases = context.Users
                .Include(x => x.Cards)
                    .ThenInclude(x => x.Purchases)
                    .ThenInclude(x => x.Game)
                .ProjectTo<UserExportDto>()
                .ToList();
            ;

            return null;
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
    }
}