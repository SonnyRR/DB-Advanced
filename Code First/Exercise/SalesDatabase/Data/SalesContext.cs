namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Config;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        {

        }
        public SalesContext(DbContextOptions options) 
            : base(options)
        {
        }

        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Sale> Sales { get; set; }
        DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-R3F6I64\SQLEXPRESS;Initial Catalog=SalesDB;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new SaleConfig());
        }
    }
}
