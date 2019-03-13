namespace BillPaymentSystem.Models
{
    using BillPaymentSystem.Models.Enums;

    public class PaymentMethod
    {
        public int Id { get; set; }

        public PaymentType Type { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public  CreditCard CreditCard { get; set; }
    }
}
