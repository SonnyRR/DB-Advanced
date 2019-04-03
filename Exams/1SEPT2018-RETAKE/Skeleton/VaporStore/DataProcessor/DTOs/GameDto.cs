namespace VaporStore.DataProcessor.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models;

    public class GameDto : IEquatable<Game>
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        [MinLength(1)]
        public string[] Tags { get; set; }

        public bool Equals(Game other)
        {
            if (this.Name == other.Name && this.Price == other.Price
                && this.ReleaseDate == other.ReleaseDate && this.Developer == other.Developer.Name
                && this.Genre == other.Genre.Name)
            {
                return true;
            }

            else
            {
                return false;
            }

        }
    }
}
