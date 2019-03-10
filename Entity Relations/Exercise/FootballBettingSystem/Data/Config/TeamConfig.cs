namespace P03_FootballBetting.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Data.Models;

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.TeamId);

            builder.HasOne(x => x.PrimaryKitColor)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.PrimaryKitColorId);

            builder.HasOne(x => x.SecondaryKitColor)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.SecondaryKitColorId);

            builder.HasOne(x => x.Town)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.TownId);

            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired(true);

            builder.Property(x => x.Initials)
                .HasColumnType("CHAR(3)");

        }
    }
}
