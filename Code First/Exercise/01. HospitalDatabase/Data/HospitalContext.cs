namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Data.Models;

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
            builder.Entity<Patient>()
                .HasKey(x => x.PatientId);

            builder.Entity<Visitation>()
                .HasKey(x => x.VisitationId);

            builder.Entity<Visitation>()
                .HasOne(x => x.Patient)
                .WithMany(x => x.Visitations)
                .HasForeignKey(x => x.PatientId);

            builder.Entity<Diagnose>()
                .HasKey(x => x.DiagnoseId);

            builder.Entity<Diagnose>()
                .Property(x => x.Name)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Diagnose>()
                .Property(x => x.Comments)
                .HasColumnType("nvarchar(250)");

            builder.Entity<Diagnose>()
                .HasOne(x => x.Patient)
                .WithMany(x => x.Diagnoses)
                .HasForeignKey(x => x.PatientId);

            builder.Entity<Medicament>()
                .HasMany(x => x.Patient)
                .WithMany(x => x.Medicaments)
                .HasForeignKey(x => x.PatientId);

            builder.Entity<Medicament>()
                .Property(x => x.Name)
                .HasColumnType("nvarchar(50)");
        }
    }
}
