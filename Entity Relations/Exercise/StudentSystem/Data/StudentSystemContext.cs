namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using Data.EntityConfigs;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
           
        }

        public StudentSystemContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=StudentSystem;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentCourseConfig());
        }
    }
}
