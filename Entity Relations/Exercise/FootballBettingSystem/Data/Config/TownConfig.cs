namespace P03_FootballBetting.Data.Config
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(x => x.TownId);

            builder.HasMany(x => x.Teams)
                .WithOne(x => x.Town)
                .HasForeignKey(x => x.TownId);

            builder.HasOne(x => x.Country)
                .WithMany(x => x.Towns)
                .HasForeignKey(x => x.CountryId);

            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired(true);
        }
    }
}
