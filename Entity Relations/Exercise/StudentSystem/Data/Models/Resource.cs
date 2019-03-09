namespace P01_StudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Enums;

    public class Resource
    {
        public int ResourceId { get; set; }
        
        [Column(TypeName ="nvarchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName ="varchar(MAX)")]
        [Required]
        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
