namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using KotsevHelper;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2:F2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var movies = KotsevExamHelper.DeserializeObjectFromJson<List<MovieImportDto>>(jsonString)
                .AsQueryable()
                .ProjectTo<Movie>()
                .ToList();

            var mapped = new List<Movie>();
            StringBuilder builder = new StringBuilder();

            foreach (var mv in movies)
            {
                if (KotsevExamHelper.IsValid(mv) == false)
                {
                    builder.AppendLine(ErrorMessage);
                    continue;
                }

                builder.AppendLine(string.Format(SuccessfulImportMovie, mv.Title, mv.Genre, mv.Rating));
                mapped.Add(mv);
            }

            context.Movies.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var hallSeats = KotsevExamHelper.DeserializeObjectFromJson<List<HallImportDto>>(jsonString);

            StringBuilder builder = new StringBuilder();

            var mapped = new List<Hall>();

            foreach (var hs in hallSeats)
            {
                if (KotsevExamHelper.IsValid(hs) == false)
                {
                    builder.AppendLine(ErrorMessage);
                    continue;
                }

                var currentHall = Mapper.Map<Hall>(hs);

                for (int i = 1; i <= hs.Seats; i++)
                {
                    var currentSeat = new Seat()
                    {
                        Hall = currentHall,
                    };

                    currentHall.Seats.Add(currentSeat);
                }

                mapped.Add(currentHall);
                string type = currentHall.Is4Dx && currentHall.Is3D ? "4Dx/3D" : !currentHall.Is3D && !currentHall.Is4Dx ? "Normal" : currentHall.Is3D ? "3D" : "4Dx";

                builder.AppendLine(string.Format(SuccessfulImportHallSeat, currentHall.Name, type, currentHall.Seats.Count));
            }

            context.Halls.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var doc = XDocument.Parse(xmlString)
                .Root
                .Elements()
                .ToList();

            StringBuilder builder = new StringBuilder();
            var mapped = new List<Projection>();

            foreach (var elProj in doc)
            {
                var movieId = int.Parse(elProj.Element("MovieId").Value);
                var movie = KotsevExamHelper.GetObjectFromSet<Movie, CinemaContext>(x => x.Id == movieId, context);

                if (movie == null)
                {
                    builder.AppendLine(ErrorMessage);
                    continue;
                }

                var hallId = int.Parse(elProj.Element("HallId").Value);
                var hall = KotsevExamHelper.GetObjectFromSet<Hall, CinemaContext>(x => x.Id == hallId, context);

                if (hall == null)
                {
                    builder.AppendLine(ErrorMessage);
                    continue;
                }

                var projDate = DateTime.ParseExact(elProj.Element("DateTime").Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var currentProjection = new Projection()
                {
                    Movie = movie,
                    Hall = hall,
                    DateTime = projDate
                };

                mapped.Add(currentProjection);
                builder.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, projDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(mapped);
            context.SaveChanges();
            return builder.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var doc = XDocument.Parse(xmlString)
                .Root
                .Elements()
                .ToList();

            StringBuilder builder = new StringBuilder();
            var mapped = new List<Customer>();

            foreach (var csElement in doc)
            {
                var firstName = csElement.Element("FirstName").Value;
                var lastName = csElement.Element("LastName").Value;
                var age = int.Parse(csElement.Element("Age").Value);
                var balance = decimal.Parse(csElement.Element("Balance").Value);

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)
                    || age < 12 || age > 110 || balance < 0.01m)
                {
                    builder.AppendLine(ErrorMessage);
                    continue;
                }

                var currentCustomer = new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Balance = balance
                };

                foreach (var ticketElement in csElement.Element("Tickets").Elements())
                {
                    var currentProjId = int.Parse(ticketElement.Element("ProjectionId").Value);
                    var currentPrice = decimal.Parse(ticketElement.Element("Price").Value);

                    var ticket = new Ticket()
                    {
                        ProjectionId = currentProjId,
                        Price = currentPrice
                    };

                    currentCustomer.Tickets.Add(ticket);
                }

                mapped.Add(currentCustomer);
                builder.AppendLine(string.Format(SuccessfulImportCustomerTicket, currentCustomer.FirstName, currentCustomer.LastName, currentCustomer.Tickets.Count));
            }

            context.Customers.AddRange(mapped);
            context.SaveChanges();

            return builder.ToString().TrimEnd();
        }
    }
}