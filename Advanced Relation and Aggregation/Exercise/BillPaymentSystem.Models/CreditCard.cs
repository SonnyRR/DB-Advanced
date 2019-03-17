namespace BillPaymentSystem.Models
{
    using BillPaymentSystem.Models.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {

        [Key]
        public int CreditCardId { get; set; }

        [Expiration]
        public DateTime ExpirationDate { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Limit { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal MoneyOwed { get; set; }

        public decimal LimitLeft
        {
            get { return this.Limit - this.MoneyOwed; }
        }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
