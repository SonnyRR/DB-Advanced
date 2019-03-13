namespace BillPaymentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BillPaymentSystem.Models;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(x => x.BankName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SWIFT)
                .IsUnicode(false)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
