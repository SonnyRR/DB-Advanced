namespace VaporStore.DataProcessor.DTOs.Export
{
    using System;
    using System.Collections.Generic;

    public class GenreExportDto
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public List<GameExportDto> MyProperty { get; set; }
    }
}
