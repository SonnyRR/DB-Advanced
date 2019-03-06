namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using P01_HospitalDatabase.Data.ModelConfigs;

    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        protected HospitalContext()
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new PatientConfig());

        }
    }
}
