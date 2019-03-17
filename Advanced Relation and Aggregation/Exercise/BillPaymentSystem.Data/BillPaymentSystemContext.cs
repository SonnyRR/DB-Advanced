namespace BillPaymentSystem.Data
{
    using Microsoft.EntityFrameworkCore;

    using Data.EntityConfigurations;
    using Models;

    public class BillPaymentSystemContext : DbContext
    {
        public BillPaymentSystemContext()
        {
        }

        public BillPaymentSystemContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(ConnectionConfig.CON_STR);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());   
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());   
            modelBuilder.ApplyConfiguration(new PaymentMethodConfig());   
        }
    }
}
