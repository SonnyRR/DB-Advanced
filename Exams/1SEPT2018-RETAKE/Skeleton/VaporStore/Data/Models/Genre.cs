namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre : IEquatable<Genre>
    {

        public Genre()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }

        public bool Equals(Genre other)
        {
            if (this.Name == other.Name)
                return true;

            else
                return false;
        }
    }
}
