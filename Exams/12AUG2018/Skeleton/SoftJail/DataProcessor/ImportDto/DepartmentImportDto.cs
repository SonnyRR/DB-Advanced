namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class DepartmentImportDto
    {

        [Required]
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        [MinLength(1)]
        public CellImportDto[] Cells { get; set; }

    }
}
