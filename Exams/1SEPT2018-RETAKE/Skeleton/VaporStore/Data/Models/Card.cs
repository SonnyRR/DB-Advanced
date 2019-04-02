namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Enums;

    public class Card
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"^(?<groups>[\d]{4} [\d]{4} [\d]{4} [\d]{4})$")]
        public string Number { get; set; }

        [RegularExpression(@"^[\d]{3}$")]
        public string Cvc { get; set; }

        public CardType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Purchase> Purchases  { get; set; }
    }
}
