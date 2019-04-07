namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using KotsevHelper;
    using Microsoft.EntityFrameworkCore;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                .Where(x => x.Rating >= rating && x.Projections.Select(z => z.Tickets).Any())
                .Take(10)
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.Projections.Sum(z => z.Tickets.Sum(w => w.Price)))
                //.ProjectTo<MovieExportDto>()
                .Select(x => new
                {
                    MovieName = x.Title,
                    Rating = $"{x.Rating:F2}",
                    TotalIncomes = $"{x.Projections.Sum(z => z.Tickets.Sum(w => w.Price)):F2}",
                    Customers = x.Projections
                        .SelectMany(z => z.Tickets).Select(w => new
                        {
                            FirstName = w.Customer.FirstName,
                            LastName = w.Customer.LastName,
                            Balance = $"{w.Customer.Balance:F2}"
                        })
                        .OrderByDescending(w => w.Balance)
                        .ThenBy(w => w.FirstName)
                        .ThenBy(w => w.LastName)
                        .ToArray()
                })
                .ToList();           

            var json = KotsevExamHelper.SerializeObjectToJson(movies);
            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(x => x.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(y => y.Price))
                .Take(10)
                .ProjectTo<CustomerExportDto>()
                .ToList();

            var xml = KotsevExamHelper.SerializeObjectToXml<List<CustomerExportDto>>(customers, "Customers");
            return xml;

        }
    }
}