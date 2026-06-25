using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using University2.Models;

namespace University2.ClassMapping
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasOne(g => g.Assignment)
                .WithMany(a => a.Grades)
                .HasForeignKey(g => g.AssignmentId);

            builder.HasOne(g => g.Student)
                   .WithMany(u => u.Grades)
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
