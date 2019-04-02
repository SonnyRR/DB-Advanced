namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Enums;

    public class Purchase
    {

        [Key]    
        public int Id { get; set; }

        public PurchaseType Type { get; set; }

        [RegularExpression(@"^(?<productKey>[A-Z\d]{4}-[A-Z\d]{4}-[A-Z\d]{4})$")]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}
