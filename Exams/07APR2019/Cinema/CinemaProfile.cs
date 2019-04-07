using AutoMapper;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ExportDto;
using Cinema.DataProcessor.ImportDto;
using System;
using System.Globalization;
using System.Linq;

namespace Cinema
{
    public class CinemaProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public CinemaProfile()
        {

            // MovieImport
            CreateMap<MovieImportDto, Movie>()
                .ForMember(x => x.Genre, y => y.MapFrom(src => Enum.Parse<Genre>(src.Genre)));

            // HallImport
            CreateMap<HallImportDto, Hall>()
                .ForMember(x => x.Seats, y => y.Ignore());

            // TopMovie Export
            CreateMap<Movie, MovieExportDto>()
                .ForMember(x => x.MovieName, y => y.MapFrom(src => src.Title))
                .ForMember(x => x.Rating, y => y.MapFrom(src => $"{src.Rating:F2}"))
                .ForMember(x => x.TotalIncomes, y => y.MapFrom(src => $"{src.Projections.Sum(z => z.Tickets.Sum(w => w.Price)):F2}"))
                .ForMember(x => x.Customers,
                    y => y.MapFrom(src => src.Projections
                    .SelectMany(w => w.Tickets
                        .Select(z => z.Customer)
                        //.OrderByDescending(q => $"{q.Balance:F2}")
                        //.OrderBy(q => q.FirstName)
                        //.ThenBy(q => q.LastName
                        )));

            CreateMap<Customer, CustomerMovieExportDto>()
                .ForMember(x => x.Balance, y => y.MapFrom(src => $"{src.Balance:F2}"));

            // TopCustomers
            CreateMap<Customer, CustomerExportDto>()
                .ForMember(x => x.SpentMoney, y => y.MapFrom(z => $"{z.Tickets.Sum(w => w.Price):F2}"))
                .ForMember(x => x.SpentTime, y => 
                    y.MapFrom(z => new TimeSpan(z.Tickets.Sum(w => w.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture)));

        }
    }
}
