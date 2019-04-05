namespace VaporStore
{
    using System;
    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.DTOs.Export;
    using System.Globalization;

    public class VaporStoreProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public VaporStoreProfile()
        {
            #region JSON Export mappings

            CreateMap<Genre, GenreExportDto>()
                .ForMember(x => x.Genre, y => y.MapFrom(obj => obj.Name))
                .ForMember(x => x.Games,
                    y => y.MapFrom(obj => obj.Games.OrderByDescending(z => z.Purchases.Count).ThenBy(z => z.Id)))
                .ForMember(x => x.TotalPlayers, y => y.MapFrom(obj => obj.Games.Sum(z => z.Purchases.Count)));

            CreateMap<Game, GameExportDto>()
                .ForMember(x => x.Title, y => y.MapFrom(obj => obj.Name))
                .ForMember(x => x.Developer, y => y.MapFrom(obj => obj.Developer.Name))
                .ForMember(x => x.Tags, y => y.MapFrom(obj => string.Join(", ", obj.GameTags.Select(z => z.Tag.Name))))
                .ForMember(x => x.Players, y => y.MapFrom(obj => obj.Purchases.Count));

            #endregion

            #region XML Export mappings

            CreateMap<User, UserExportDto>()
                .ForMember(x => x.Purchases, y => y.MapFrom(obj => obj.Cards.SelectMany(z => z.Purchases).OrderBy(z => z.Date)))
                .ForMember(x => x.TotalSpent, y => y.MapFrom(obj => obj.Cards.SelectMany(z => z.Purchases).Sum(z => z.Game.Price)));

            CreateMap<Purchase, PurchaseExportDto>()
                .ForMember(x => x.Card, y => y.MapFrom(obj => obj.Card.Number))
                .ForMember(x => x.Game, y => y.MapFrom(obj => obj.Game))
                .ForMember(x => x.Date, y => y.MapFrom(obj => obj.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Cvc, y => y.MapFrom(obj => obj.Card.Cvc));

            CreateMap<Game, GameExportDtoXML>()
                .ForMember(x => x.Genre, y => y.MapFrom(obj => obj.Genre.Name))
                .ForMember(x => x.Title, y => y.MapFrom(obj => obj.Name));

            #endregion
        }
    }
}