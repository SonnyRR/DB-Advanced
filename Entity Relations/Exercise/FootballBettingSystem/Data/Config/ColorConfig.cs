namespace P03_FootballBetting.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.ColorId);

            builder.HasMany(x => x.Teams)
                .WithOne(x => x.PrimaryKitColor)
                .HasForeignKey(x => x.PrimaryKitColorId);

            builder.HasMany(x => x.Teams)
                .WithOne(x => x.SecondaryKitColor)
                .HasForeignKey(x => x.SecondaryKitColorId);

            builder.Property(x => x.Name)
                .HasMaxLength(15)
                .IsRequired(true);
        }
    }
}
