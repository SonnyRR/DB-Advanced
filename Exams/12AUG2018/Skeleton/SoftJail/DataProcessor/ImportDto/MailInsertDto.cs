namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class MailInsertDto
    {

        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [RegularExpression(@"^[A-Za-z\d ]+ str\.$")]
        public string Address { get; set; }
    }
}
