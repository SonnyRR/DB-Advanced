namespace VaporStore.DataProcessor.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CardDto
    {
        [RegularExpression(@"^(?<groups>[\d]{4} [\d]{4} [\d]{4} [\d]{4})$")]
        public string Number { get; set; }

        [RegularExpression(@"^[\d]{3}$")]
        public string Cvc { get; set; }

        public string Type { get; set; }
    }
}
