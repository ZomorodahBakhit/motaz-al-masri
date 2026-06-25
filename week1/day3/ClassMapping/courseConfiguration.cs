using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using University2.Models;

namespace University2.ClassMapping
{
    public class courseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(150);
            builder.HasOne(c => c.Syllabus)
                .WithOne(s => s.course)
                .HasForeignKey<Course>(c => c.SyllabusId);

            builder.HasOne(c => c.Teacher)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
