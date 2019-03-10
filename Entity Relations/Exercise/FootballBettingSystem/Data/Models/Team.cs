namespace P03_FootballBetting.Data.Models
{
    using System;

    public class Team
    {
        public Team()
        {
        }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }


        public int TownId { get; set; }
    }
}
