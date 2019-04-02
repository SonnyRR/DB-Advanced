using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
	public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }    
        public Developer Developer{ get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<GameTag> GameTags { get; set; }

    }
}
