namespace BillPaymentSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SWIFT { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
