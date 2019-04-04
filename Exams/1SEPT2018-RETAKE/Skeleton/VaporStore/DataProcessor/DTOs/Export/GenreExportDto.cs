namespace VaporStore.DataProcessor.DTOs.Export
{
    using System.Collections.Generic;

    public class GenreExportDto
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public List<GameExportDto> Games { get; set; } = new List<GameExportDto>();

        public int TotalPlayers { get; set; }
    }
}
