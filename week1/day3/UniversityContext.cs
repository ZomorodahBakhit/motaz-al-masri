using Microsoft.EntityFrameworkCore;
using University2.ClassMapping;
using University2.Models;
namespace University2
{
    public class UniversityContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Syllabus> Syllabus { get; set; } 
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=UniversityDB_EF;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new userConfiguration());
            modelBuilder.ApplyConfiguration(new courseConfiguration());
            modelBuilder.ApplyConfiguration(new SyllabusConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
        }

    }
}
