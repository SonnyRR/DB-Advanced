namespace BillPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        [Column(TypeName = "DECIMAL(15,4)")]
        public decimal Balance { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        public string SWIFT { get; set; }


    }
}
