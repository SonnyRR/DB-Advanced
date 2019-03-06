namespace P01_HospitalDatabase.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
