namespace BookShop
{
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
              //DbInitializer.ResetDatabase(db);

                Console.WriteLine(GetBooksByAuthor(db, "R"));
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            var books = context.Books
                .Where(x => string.Compare(x.AgeRestriction.ToString(), command, true) == 0)
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.EditionType == Models.Enums.EditionType.Gold && x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => $"{x.Title} - ${x.Price:F2}")
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var splitted = input
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
                .Where(x => x.BookCategories
                            .All(y => splitted.Any(z => string.Compare(y.Category.Name, z, true) == 0)))
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var desiredDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(x => x.ReleaseDate.Value < desiredDate)
                .OrderByDescending(x => x.ReleaseDate.Value)
                .Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:F2}")
                .ToList();

            return string.Join(Environment.NewLine, books);

        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(x => x.FirstName.EndsWith(input))
                .Select(x => $"{x.FirstName} {x.LastName}")
                .ToList();

            return string.Join(Environment.NewLine, authors.OrderBy(x => x));
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context
                .Books
                .Include(x => x.Author)
                .Where(x => x.Author.LastName.StartsWith(input, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.BookId)
                .Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})")
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .ToList()
                .Count;

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    FullName = $"{x.FirstName} {x.LastName}",
                    BookCopies = x.Books.Sum(y => y.Copies),

                }
                )
                .OrderByDescending(x => x.BookCopies)
                .Select(x => $"{x.FullName} - {x.BookCopies}")
                .ToList();


            return string.Join(Environment.NewLine, authors);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Profits = c.CategoryBooks
                               .Select(x => x.Book.Price * x.Book.Copies)
                               .Sum()
                }
                )
                .ToList()
                .OrderByDescending(c => c.Profits)
                .ThenBy(c => c.CategoryName)
                .Select(x => $"{x.CategoryName} ${x.Profits}")
                .ToArray();

            return string.Join(Environment.NewLine, categories);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder builder = new StringBuilder();

            var categories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    MostRecenBooks = c.CategoryBooks
                        .Select(b => new
                        {
                            BookTitle = b.Book.Title,
                            ReleaseDate = b.Book.ReleaseDate.Value
                        })
                        .OrderByDescending(d => d.ReleaseDate)
                        .Take(3)
                        .ToList()
                })
                .ToList()
                .OrderBy(c => c.CategoryName)
                .ToList();

            foreach (var category in categories)
            {
                builder.AppendLine($"--{ category.CategoryName}");

                foreach (var book in category.MostRecenBooks)
                {
                    builder.AppendLine($"{book.BookTitle} ({book.ReleaseDate.Year})");
                }
            }

            return builder.ToString();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            books.ForEach(b => b.Price += 5);

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }
    }
}
