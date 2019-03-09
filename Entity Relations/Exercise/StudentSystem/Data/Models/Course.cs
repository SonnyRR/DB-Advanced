namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {

        public Course()
        {
            this.Resources = new HashSet<Resource>();
            this.HomeworkSubmissions = new HashSet<Homework>();
            this.StudentsEnrolled = new HashSet<StudentCourse>();
        }

        [Key]
        public int CourseId { get; set; }

        [Column(TypeName ="nvarchar(80)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName ="nvarchar(MAX)")]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }


        public ICollection<Resource> Resources { get; set; }
        public ICollection<StudentCourse> StudentsEnrolled { get; set; }
        public ICollection<Homework> HomeworkSubmissions { get; set; }
    }
}
