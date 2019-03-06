namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.PatientId);

            builder.HasMany(x => x.Diagnoses)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);

            builder.HasMany(x => x.Visitations)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsUnicode(true)
            .IsRequired(true);

            builder.Property(x => x.Address)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired(true);

            builder.Property(x => x.Email)
            .HasMaxLength(80)
            .IsUnicode(false)
            .IsRequired(true);

        }
    }
}
