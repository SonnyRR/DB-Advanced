namespace VaporStore.DataProcessor.DTOs
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public class PurchaseDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        [RegularExpression(@"^(?<productKey>[A-Z\d]{4}-[A-Z\d]{4}-[A-Z\d]{4})$")]
        [Required]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        [RegularExpression(@"^(?<groups>[\d]{4} [\d]{4} [\d]{4} [\d]{4})$")]
        [Required]
        public string Card { get; set; }
    }
}
