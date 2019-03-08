namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.DoctorId);

            builder.HasMany(x => x.Visitations)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.VisitationId);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(x => x.Specialty)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
