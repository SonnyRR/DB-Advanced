namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {

        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(x => new { x.PatientId, x.MedicamentId });
        }
    }
}
