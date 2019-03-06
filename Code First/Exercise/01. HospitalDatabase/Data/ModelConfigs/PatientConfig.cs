namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.PatientId);

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
            .IsRequired(true)

        }
    }
}
