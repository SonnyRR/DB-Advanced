namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(x => x.VisitationId);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Visitations)
                .HasForeignKey(x => x.PatientId);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Visitations)
                .HasForeignKey(x => x.DoctorId);

            builder.Property(x => x.Comments)
                .HasMaxLength(250)
                .IsRequired(true);

        }
    }
}
