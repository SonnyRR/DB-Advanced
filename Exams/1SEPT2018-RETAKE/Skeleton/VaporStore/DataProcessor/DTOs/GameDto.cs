using System;

namespace VaporStore.DataProcessor.DTOs
{
    public class GameDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Developer { get; set; }

        public string Genre { get; set; }

        public string[] Tags { get; set; }
    }
}
