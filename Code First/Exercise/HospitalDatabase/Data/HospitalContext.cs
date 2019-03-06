namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    using Data.ModelConfigs;
    using Data.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public HospitalContext()
        {

        }

        DbSet<Patient> Patients { get; set; }
        DbSet<Visitation> Visitations { get; set; }
        DbSet<Diagnose> Diagnoses { get; set; }
        DbSet<Medicament> Medicaments { get; set; }
        DbSet<PatientMedicament> PatientMedicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=HospitalDB;User Id=sa;Password=EmanuelaTop1.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new PatientConfig());
            builder.ApplyConfiguration(new VisitationConfig());
            builder.ApplyConfiguration(new DiagnoseConfig());
            builder.ApplyConfiguration(new MedicamentConfig());
            builder.ApplyConfiguration(new PatientMedicamentConfig());
        }
    }
}
