namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {

        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [RegularExpression(@"^(?<name>[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+)$")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }

    }
}
