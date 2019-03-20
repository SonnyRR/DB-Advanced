namespace BillPaymentSystem.Data.EntityConfigurations
{
    using System;
    using BillPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(x => x.User)
            //    .WithMany(x => x.PaymentMethods)
            //    .HasForeignKey(x => x.UserId);
            //
            //builder.HasOne(x => x.BankAccount)
            //    .WithOne(x => x.PaymentMethod)
            //    .HasForeignKey<PaymentMethod>(x => x.BankAccountId);
            //
            //builder.HasOne(x => x.CreditCard)
            //     .WithOne(x => x.PaymentMethod)
            //     .HasForeignKey<PaymentMethod>(x => x.CreditCardId);
        }
    }
}
