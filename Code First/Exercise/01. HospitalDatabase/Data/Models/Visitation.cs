namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Visitation
    {
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        public int MyProperty { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
