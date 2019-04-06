namespace SoftJail.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CellImportDto : IEqualityComparer<CellImportDto>
    {

        [Range(1, 1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }

        public bool Equals(CellImportDto x, CellImportDto y)
        {
            if (x.CellNumber == y.CellNumber)
                return true;


            return false;
        }

        public int GetHashCode(CellImportDto obj)
        {
            return (CellNumber ^ 4) * 20412 + 23;
        }
    }
}
