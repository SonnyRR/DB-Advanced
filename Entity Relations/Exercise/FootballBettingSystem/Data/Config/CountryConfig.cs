namespace P03_FootballBetting.Data.Config
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.CountryId);

            builder.HasMany(x => x.Towns)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId);

            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired(true);
        }
    }
}
