namespace BillPaymentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {

        public User()
        {
            this.PaymentMethods = new HashSet<PaymentMethod>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }   
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
