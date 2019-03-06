namespace P01_HospitalDatabase.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class PatientMedicamentsConfig : IEntityTypeConfiguration<PatientMedicaments>
    {

        public void Configure(EntityTypeBuilder<PatientMedicaments> builder)
        {
            builder.HasKey(x => new { x.PatientId, x.MedicamentId });
        }
    }
}
