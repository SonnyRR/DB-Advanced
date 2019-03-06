namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(x => x.MedicamentId);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
