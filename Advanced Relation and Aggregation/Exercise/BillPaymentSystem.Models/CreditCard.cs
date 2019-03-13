namespace BillPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {

        [Key]
        public int CreditCardId { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Column(TypeName = "DECIMAL(15,4)")]
        public decimal Limit { get; set; }

        [Column(TypeName = "DECIMAL(15,4)")]
        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft
        {
            get { return LimitLeft - MoneyOwed; }
        }


        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
