namespace P01_HospitalDatabase.Data.ModelConfigs
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {

        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder.HasKey(x => x.DiagnoseId);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Diagnoses)
                .HasForeignKey(x => x.PatientId);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(x => x.Comments)
                .HasMaxLength(250)
                .IsRequired(true)
                .IsRequired(true);
        }
    }
}
