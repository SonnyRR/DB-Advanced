namespace BookShop.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;
    using EntityConfiguration;
    using System;
    using System.Runtime.InteropServices;

    public class BookShopContext : DbContext
    {
        public BookShopContext() { }

        public BookShopContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookCategory> BooksCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    optionsBuilder.UseSqlServer(Configuration.ConnectionStringWindows);

                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    optionsBuilder.UseSqlServer(Configuration.ConnectionStringMacOS);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
