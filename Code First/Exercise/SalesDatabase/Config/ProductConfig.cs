namespace P03_SalesDatabase.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder.HasMany(x => x.Sales)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");

        }
    }
}
