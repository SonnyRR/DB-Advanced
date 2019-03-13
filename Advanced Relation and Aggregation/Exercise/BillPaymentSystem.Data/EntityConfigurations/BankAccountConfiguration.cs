namespace BillPaymentSystem.Data.EntityConfigurations
{
    using BillPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(x => x.BankName)
                .HasMaxLength(50);

            builder.Property(x => x.SWIFT)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}
