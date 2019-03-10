namespace P03_FootballBetting.Data.Config
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PositionConfig : IEntityTypeConfiguration<Position>
    {

        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.PositionId);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Position)
                .HasForeignKey(x => x.PositionId);
        }
    }
}
