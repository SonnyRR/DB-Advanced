namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }

    }
}
