namespace Cinema.DataProcessor.ExportDto
{
    using System.Collections.Generic;

    public class MovieExportDto
    {
        public string MovieName { get; set; }

        public string Rating { get; set; }

        public string TotalIncomes { get; set; }

        public List<CustomerMovieExportDto> Customers { get; set; }
    }
}
