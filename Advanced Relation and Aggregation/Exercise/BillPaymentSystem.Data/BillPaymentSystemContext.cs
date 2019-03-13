namespace BillPaymentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Data.EntityConfigurations;

    public class BillPaymentSystemContext : DbContext
    {
        public BillPaymentSystemContext()
        {
        }

        public BillPaymentSystemContext(DbContextOptions options) 
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());   
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());   
        }
    }
}
