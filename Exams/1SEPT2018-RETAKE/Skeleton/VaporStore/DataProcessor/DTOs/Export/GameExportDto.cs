namespace VaporStore.DataProcessor.DTOs.Export
{
    public class GameExportDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Developer { get; set; }

        public string[] Tags { get; set; }

        public int Players { get; set; }
    }
}
