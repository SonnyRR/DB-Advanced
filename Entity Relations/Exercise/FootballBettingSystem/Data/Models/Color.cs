namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;

    public class Color
    {
        public Color()
        {
            this.Teams = new HashSet<Team>();
        }

        public int ColorId { get; set; }

        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
