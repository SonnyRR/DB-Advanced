namespace BillPaymentSystem.Data.EntityConfigurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BillPaymentSystem.Models;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x=>x.CreditCard)
                .WithOne(x=>x.)
        }
    }
}
