namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {

        public Genre()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }        
    }
}
