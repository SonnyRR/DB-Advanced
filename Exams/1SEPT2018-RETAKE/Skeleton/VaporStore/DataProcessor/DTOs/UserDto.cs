namespace VaporStore.DataProcessor.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [RegularExpression(@"^(?<name>[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+)$")]
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        [MinLength(1)]
        public CardDto[] Cards { get; set; }
    }
}
